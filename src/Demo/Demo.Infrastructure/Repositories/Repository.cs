using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Domain.Entities;
using Demo.Domain.Repositories;

namespace Demo.Infrastructure.Repositories
{
    public abstract class Repository<TAggregateRoot, Tkey>
                         : IRepository<TAggregateRoot, Tkey>
                         where TAggregateRoot : class , IAggregateRoot<Tkey>
                         where Tkey : IComparable
    {
        private ApplicationDbContext _dbContext;
        private DbSet<TAggregateRoot> _dbset;
        public Repository(ApplicationDbContext context)
        {
            _db
        }

        public void Add(TAggregateRoot entityToAdd)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TAggregateRoot entityToAdd)
        {
            throw new NotImplementedException();
        }

        public void Edit(TAggregateRoot entityToEdit)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(TAggregateRoot entityToEdit)
        {
            throw new NotImplementedException();
        }

        public IList<TAggregateRoot> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<TAggregateRoot>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot GetById(Tkey id)
        {
            throw new NotImplementedException();
        }

        public Task<TAggregateRoot> GetByIdAsync(Tkey id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(Expression<Func<TAggregateRoot, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<TAggregateRoot, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expression<Func<TAggregateRoot, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(TAggregateRoot entityToRemove)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tkey id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Expression<Func<TAggregateRoot, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TAggregateRoot entityToRemove)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Tkey id)
        {
            throw new NotImplementedException();
        }

        public void Update(TAggregateRoot entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TAggregateRoot entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
