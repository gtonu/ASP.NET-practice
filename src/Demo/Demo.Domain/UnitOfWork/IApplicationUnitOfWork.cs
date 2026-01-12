using Demo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.UnitOfWork
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
    }
}
