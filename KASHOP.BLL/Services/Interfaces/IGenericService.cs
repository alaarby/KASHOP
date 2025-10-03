﻿using KASHOP.DAL.DTOs.Requests;
using KASHOP.DAL.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IGenericService<TRequest, TResponse, TEntity>
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool onlyActive = false);
        TResponse? GetById(int id);
        int Update(int id, TRequest request);
        int Delete(int id);
        public bool Toggle(int id);
    }
}
