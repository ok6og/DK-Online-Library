using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.CommandHandlers.AuthorCommandHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand,Author>
    {
        private readonly IAuthorRepository _authorRepo;


        public AddAuthorCommandHandler(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.AddUser(request.author);
        }
    }
}
