using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers.AuthorCommandHandlers
{
    public class GetByIdAuthorCommandHandler : IRequestHandler<GetByIdAuthorCommand, Author>
    {
        private IAuthorRepository _authorService;

        public GetByIdAuthorCommandHandler(IAuthorRepository authorService)
        {
            _authorService = authorService;
        }

        public async Task<Author> Handle(GetByIdAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorService.GetById(request.authorId);
        }
    }
}
