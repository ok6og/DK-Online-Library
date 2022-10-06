using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands;
using DK_Project.Models.Mediatr.Commands.BookCommands;
using DK_Project.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookCommandHandlers
{
    public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, Book>
    {
        private IBookRepository _bookRepository;

        public GetByIdCommandHandler(IBookRepository bookRepo)
        {
            _bookRepository = bookRepo;
        }

        public async Task<Book> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetById(request.bookId);
        }
    }
}
