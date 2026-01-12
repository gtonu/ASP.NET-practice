using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using Demo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Repositories
{
    public class BookRepository : Repository<Books, Guid>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
