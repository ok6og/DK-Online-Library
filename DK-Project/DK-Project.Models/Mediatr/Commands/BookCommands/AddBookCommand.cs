using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;
using MediatR;

namespace DK_Project.Models.Mediatr.Commands.BookCommands
{
    public record AddBookCommand(Book book) : IRequest<Book>
    {
    }
}
