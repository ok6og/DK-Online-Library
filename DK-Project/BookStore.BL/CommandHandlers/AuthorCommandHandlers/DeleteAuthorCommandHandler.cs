using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.MsSql;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace BookStore.BL.CommandHandlers.AuthorCommandHandlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IBookRepository _bookRepository;



        public DeleteAuthorCommandHandler(IAuthorRepository authorRepo, IBookRepository bookRepository)
        {
            _authorRepo = authorRepo;
            _bookRepository = bookRepository;
        }

        public async Task<Author> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            if (await _bookRepository.DoesAuthorHaveBooks(request.authorId))
            {
                return null;
            }
            return await _authorRepo.DeleteUser(request.authorId);
        }
    }
}
