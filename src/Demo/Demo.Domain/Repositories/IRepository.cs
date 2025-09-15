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
        void Add(TAggregateRoot entityToAdd);
        Task AddAsync(TAggregateRoot entityToAdd);
        void Edit(TAggregateRoot entityToEdit);
        Task EditAsync(TAggregateRoot entityToEdit);
        IList<TAggregateRoot> GetAll();
        Task<IList<TAggregateRoot>> GetAllAsync();
        TAggregateRoot GetById(Tkey id);
        Task<TAggregateRoot> GetByIdAsync(Tkey id);
        int GetCount(Expression<Func<TAggregateRoot, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TAggregateRoot, bool>> filter = null);
        void Remove(Expression<Func<TAggregateRoot, bool>> filter);
        void Remove(TAggregateRoot entityToRemove);
        void Remove(Tkey id);
        Task RemoveAsync(Expression<Func<TAggregateRoot, bool>> filter);
        Task RemoveAsync(TAggregateRoot entityToRemove);
        Task RemoveAsync(Tkey id);
        void Update(TAggregateRoot entityToUpdate);
        Task UpdateAsync(TAggregateRoot entityToUpdate);

    }
}
