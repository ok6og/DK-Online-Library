using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands.BookCommands;
using DK_Project.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookCommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IBookRepository _bookRepo;

        public UpdateBookCommandHandler(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.UpdateBook(request.book);
        }
    }
}
