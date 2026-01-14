using Cortex.Mediator.Commands;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Command
{
    public class CreateBookCommand : ICommand<Book>
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal? Price { get; set; }
        public string AuthorName { get; set; } = null!;
        public int InStock { get; set; }

    }
}
