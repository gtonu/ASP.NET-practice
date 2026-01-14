using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Book : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal? Price { get; set; }
        public string AuthorName { get; set; } = null!;
        public int? InStock { get; set; }

    }
}
