using Demo.Domain.Repositories;
using Demo.Domain.UnitOfWork;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.UnitOfWork
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IBookRepository BookRepository { get; private set; }
        public ApplicationUnitOfWork(ApplicationDbContext context,IBookRepository bookRepository) 
            : base(context)
        {
            BookRepository = bookRepository;
        }
    }
}
