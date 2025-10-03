using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public CheckOutService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IEmailSender emailSender,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderIdAsync(orderId);

            var subject = "";
            var body = "";

            if(order.PaymnetMethod == DAL.Enums.PaymnetMethodEnum.Visa)
            {
                order.Status = DAL.Enums.OrderStatus.Approved;

                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                
                var OrderItems = new List<OrderItem>();
                var productUpdated = new List<(int productId, int quantity)>();

                foreach(var cartItem in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = orderId,
                        ProductId = cartItem.ProductId,
                        TotalPrice = cartItem.Product.Price * cartItem.Count,
                        Count = cartItem.Count,
                        Price = cartItem.Product.Price,
                    };
                    OrderItems.Add(orderItem);
                    productUpdated.Add((cartItem.ProductId, cartItem.Count));
                }
                await _orderItemRepository.AddRangeAsync(OrderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                await _productRepository.DecreaseQuantityAsync(productUpdated);

                subject = "Payment Successful - kashop";
                body = $"<h1>thank you for your paymnet</h1>" +
                       $"<p>your payment for order {orderId}</p>" +
                       $"<p>total Amount : {order.TotalAmount}</p>";
            }
            else if(order.PaymnetMethod == DAL.Enums.PaymnetMethodEnum.Cash)
            {
                subject = "Order Placed Successfully - kashop";
                body = $"<h1>thank you for your order</h1>" +
                       $"<p>total Amount : {order.TotalAmount}</p>";
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);

            return true;
        }

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cart = await _cartRepository.GetUserCartAsync(UserId);

            if (!cart.Any()) 
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty",
                };
            }

            Order order = new Order
            {
                UserId = UserId,
                PaymnetMethod = request.PaymentMethod,
                TotalAmount = cart.Sum(c => c.Product.Price * c.Count)
            };

            await _orderRepository.AddAsync(order);

            if(request.PaymentMethod == DAL.Enums.PaymnetMethodEnum.Cash)
            {
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Cash"
                };
            }
            else if(request.PaymentMethod == DAL.Enums.PaymnetMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        
                    },
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/Success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                };

                foreach(var item in cart)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);

                order.PaymentId = session.Id;

                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment session created successfully.",
                    PaymentId = session.Id,
                    Url = session.Url,
                };
            }

            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid"
            };
        }
    }
}
