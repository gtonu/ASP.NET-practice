using Cortex.Mediator.Commands;
using Demo.Domain.Entities;
using Demo.Domain.UnitOfWork;
using Demo.Domain.Utilities;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Command
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Book>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IApplicationUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Book> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            if(command.Title is null)
            {
                throw new ArgumentNullException(nameof(command.Title));
            }
            var book = _mapper.Map<Book>(command);
            book.Id = IdentityGenerator.NewSequentialGuid();

            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveAsync();

            return book;
        }
    }
}
