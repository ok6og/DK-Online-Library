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
    public class GetAuthorByNameCommandHandler : IRequestHandler<GetAuthorByNameCommand, Author>
    {
        private IAuthorRepository _authorRepo;

        public GetAuthorByNameCommandHandler(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<Author> Handle(GetAuthorByNameCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepo.GetAuthorByName(request.authorName);
        }
    }
}
