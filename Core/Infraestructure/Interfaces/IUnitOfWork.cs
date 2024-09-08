﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Interfaces
{
    public interface IUnitOfWork<T> where T : class
        {
            Task<IEnumerable<T>> GetAll();
            Task<T> GetById(int id);
            Task Add(T entity);
            void Update(T entity);
            void Delete(T entity);
        }
    
}
