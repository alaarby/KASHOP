﻿using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.responses;
using KASHOP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(string userId);
        Task<bool> BlockUserAsync(string userId, int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, ChangeRoleRequest request);
        Task<bool> IsBlockedAsync(string userId);
    }
}
