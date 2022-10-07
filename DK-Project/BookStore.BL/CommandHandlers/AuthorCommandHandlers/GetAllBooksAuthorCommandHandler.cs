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
    public class GetAllBooksAuthorCommandHandler : IRequestHandler<GetAllAuthorsCommand, IEnumerable<Author>>
    {
        private readonly IAuthorRepository _authorRepo;


        public GetAllBooksAuthorCommandHandler(IAuthorRepository authorService)
        {
            _authorRepo = authorService;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.GetAllUsers();
        }
    }
}
