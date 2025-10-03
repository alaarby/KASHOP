using Azure.Core;
using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTOs.Responses;
using KASHOP.DAL.Entities;
using KASHOP.DAL.Enums;
using KASHOP.DAL.Repositories;
using KASHOP.DAL.Repositories.Classes;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();

            return _repository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return 0;
            }
            return _repository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entities = _repository.GetAll();
            if (onlyActive) 
            {
                entities = entities.Where(e => e.Status == Status.Active);

            }

            return entities.Adapt<IEnumerable<TResponse>>(); ;
        }

        public TResponse? GetById(int id)
        {
            var entity = _repository.GetById(id);

            if (entity == null)
            {
                return default;
            }
            return entity.Adapt<TResponse>();
        }

        public bool Toggle(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return false;

            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;

            _repository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return 0;

            var UpdatedEntity = request.Adapt(entity);

            return _repository.Update(entity);
        }
    }
}
