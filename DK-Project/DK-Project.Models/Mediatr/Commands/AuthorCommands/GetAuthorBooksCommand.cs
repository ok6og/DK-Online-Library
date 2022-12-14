using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;
using MediatR;

namespace DK_Project.Models.Mediatr.Commands.AuthorCommands
{
    public record GetAuthorBooksCommand(int authorId) : IRequest<IEnumerable<Book>>
    {
    }
}
