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
    public class GetBookByNameCommandHandler : IRequestHandler<GetBookByNameCommand, Book>
    {
        private IBookRepository _bookRepository;

        public GetBookByNameCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByNameCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBookByName(request.bookName);
        }
    }
}
