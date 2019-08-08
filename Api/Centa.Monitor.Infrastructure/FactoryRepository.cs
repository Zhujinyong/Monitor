﻿using Centa.Monitor.Infrastructure.Interfaces;

namespace Centa.Monitor.Infrastructure
{
    /// <summary>
    /// 工厂
    /// </summary>
    public class FactoryRepository : IFactoryRepository
    {
        /// <summary>
        /// 创建Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public IRepository<T> CreateRepository<T>(IDapperContext context) where T : class
        {
            //IRepository<T> repository = new Repository<T>(context);
            //return repository;
            return null;
        }
    }
}