using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using IAuthorRepository = DK_Project.DL.Interfaces.IAuthorRepository;

namespace BookStore.BL.CommandHandlers.AuthorCommandHandlers
{
    public class GetAuthorBooksCommandHandler : IRequestHandler<GetAuthorBooksCommand, IEnumerable<Book>>
    {
        public readonly IAuthorRepository _authorRepo;
        private readonly ILogger<GetAuthorBooksCommandHandler> _logger;
        public readonly IBookRepository _bookRepository;



        public GetAuthorBooksCommandHandler(IAuthorRepository authorRepo, ILogger<GetAuthorBooksCommandHandler> logger, IBookRepository bookRepository)
        {
            _authorRepo = authorRepo;
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetAuthorBooksCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting all author{request.authorId} books");
            if (await _bookRepository.DoesAuthorHaveBooks(request.authorId))
            {
                return await _bookRepository.GetAuthorBooks(request.authorId);
            }
            _logger.LogInformation("This Author has no books");
            return null;
        }
    }
}
