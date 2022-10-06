using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands.BookCommands;
using MediatR;

namespace BookStore.BL.CommandHandlers.BookCommandHandlers
{
    public class DoesAuthorHaveBooksCommandHandler : IRequestHandler<DoesAuthorHaveBooksCommand, bool>
    {
        private IBookRepository _bookRepository;

        public DoesAuthorHaveBooksCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(DoesAuthorHaveBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.DoesAuthorHaveBooks(request.bookId);
        }
    }
}
