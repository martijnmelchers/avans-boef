using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Models.Repository.Interfaces
{
    public interface IBaseRepository<TOut>
    {
        Task<TOut> Get(int id);
        Task<List<TOut>> GetAll();

        Task<TOut> GetWhere(Expression<Func<TOut, bool>> expression);
        Task<List<TOut>> GetAllWhere(Expression<Func<TOut, bool>> expression);

        Task<TOut> Insert(TOut item);
        Task<List<TOut>> InsertAll(List<TOut> items);
        
        Task Delete(int id);
        Task DeleteWhere(Expression<Func<TOut, bool>> expression);
    }
}