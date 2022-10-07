using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.MsSql;
using DK_Project.Models.Mediatr.Commands;
using DK_Project.Models.Mediatr.Commands.BookCommands;
using DK_Project.Models.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using IAuthorRepository = DK_Project.DL.Interfaces.IAuthorRepository;

namespace BookStore.BL.CommandHandlers.BookCommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IBookRepository _bookRepo;



        public DeleteBookCommandHandler(IBookRepository bookRepo, IAuthorRepository authorRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepo.DeleteBook(request.bookId);
        }
    }
}
