using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repositories
{
    public interface IRepository<TAggregateRoot,Tkey> 
        where TAggregateRoot : class, IAggregateRoot<Tkey>
        where Tkey : IComparable
    {
        void Add(TAggregateRoot entity);
        Task AddAsync(TAggregateRoot entity);
        void Edit(TAggregateRoot entityToUpdate);
        Task EditAsync(TAggregateRoot entityToUpdate);
        IList<TAggregateRoot> GetAll();
        Task<IList<TAggregateRoot>> GetAllAsync();
        TAggregateRoot GetById(Tkey id);
        Task<TAggregateRoot> GetByIdAsync(Tkey id);
        int GetCount(Expression<Func<TAggregateRoot, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TAggregateRoot, bool>> filter = null);
        void Remove(Expression<Func<TAggregateRoot, bool>> filter);
        void Remove(TAggregateRoot entityToDelete);
        void Remove(Tkey id);
        void Update(TAggregateRoot entity);
        Task RemoveAsync(Expression<Func<TAggregateRoot, bool>> filter);
        Task RemoveAsync(TAggregateRoot entityToDelete);
        Task RemoveAsync(Tkey id);
    }
}
