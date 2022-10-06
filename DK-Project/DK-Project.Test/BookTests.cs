using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BL.Services;
using Castle.Core.Logging;
using DK_Project.AutoMapper;
using DK_Project.Controllers;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DK_Project.Test
{
    public class BookTests
    {
        private readonly IList<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id=1,
                AuthorId=1,
                LastUpdated=DateTime.Now,
                Price=10,
                Quantity=10,
                Title="The Adventure Through Time"
            },
            new Book()
            {
                Id=2,
                AuthorId=2,
                LastUpdated=DateTime.Now,
                Price=11,
                Quantity=11,
                Title="Summa Lumma Dumma"
            }
        };

        private IMapper _mapper;
        private readonly Mock<ILogger<BookService>> _logger;
        private readonly Mock<IBookRepository> _bookRepo;
        private readonly Mock<IAuthorRepository> _authorRepo;
        private readonly Mock<ILogger<BookController>> _bookControllerLoggerMock;

        public BookTests()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();
            _bookControllerLoggerMock = new Mock<ILogger<BookController>>();
            _logger = new Mock<ILogger<BookService>>();
            _authorRepo = new Mock<IAuthorRepository>();
            _bookRepo = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Book_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;
            _bookRepo.Setup(x=>x.GetAllBooks()).ReturnsAsync(_books);

            //inject
            var service = new BookService(_bookRepo.Object,_logger.Object,_authorRepo.Object);
            var controller = new BookController(service,_bookControllerLoggerMock.Object,_mapper);

            //act
            var result = await controller.Get();

            //assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);
            var books = okObjResult.Value as IEnumerable<Book>;
            Assert.NotNull(books);
            Assert.Equal(expectedCount, books.Count());
            Assert.Equal(_books, books);
        }

        [Fact]
        public async Task Book_GetAll_RightAuthors()
        {
            //setup
            _bookRepo.Setup(x => x.GetAllBooks()).ReturnsAsync(_books);
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Get();
            //assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);
            var books = okObjResult.Value as IEnumerable<Book>;
            Assert.True(books.Any());
            Assert.True(books.LastOrDefault() == _books.LastOrDefault());
            Assert.True(books.FirstOrDefault() == _books.FirstOrDefault());
        }

        [Fact]
        public async Task Book_GetBookById_OK()
        {
            //setup
            var bookId = 2;
            var expectedBook = _books.FirstOrDefault(x => x.Id == bookId);
            _bookRepo.Setup(x=> x.GetById(bookId)).ReturnsAsync(expectedBook);
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.GetByID(bookId);
            //assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);
            var book = okObjResult.Value as Book;
            Assert.NotNull(book);
            Assert.Equal(bookId, book.Id);
        }
        [Fact]
        public async Task Book_NotFound_GetById()
        {

            //setup
            var bookId = 3;
            var expectedBook = _books.FirstOrDefault(x => x.Id == bookId);
            _bookRepo.Setup(x => x.GetById(bookId)).ReturnsAsync(expectedBook);
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.GetByID(bookId);
            //assert
            var notFoundObjResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjResult);
            var returnedBook = notFoundObjResult.Value;
            Assert.NotNull(returnedBook);
            Assert.Equal("Book Doesn't Exists", returnedBook);
        }
        [Fact]
        public async Task AddBookOk()
        {
            //setup
            var bookId = 3;
            var author = new Author()
            {
                Id = 1
            };
            var bookRequest = new AddUpdateBookRequest()
            {
                AuthorId = 5,
                LastUpdated = DateTime.Now,
                Price = 10,
                Quantity = 21,
                Title = "ASDKJOSAD"
            };
            _authorRepo.Setup(x => x.GetById(bookRequest.AuthorId)).ReturnsAsync(author);
            _bookRepo.Setup(x => x.AddBook(It.IsAny<Book>())).Callback(() =>
            {
                var book = new Book()
                {
                    Id = bookId,
                    AuthorId = bookRequest.AuthorId,
                    LastUpdated = bookRequest.LastUpdated,
                    Price = bookRequest.Price,
                    Quantity = bookRequest.Quantity,
                    Title = bookRequest.Title
                };
                _books.Add(book);
            }).ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == bookId));
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Add(bookRequest);
            //assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);
            var resultValue = okObjResult.Value as Book;
            Assert.NotNull(resultValue);
            Assert.Equal(3, resultValue.Id);
        }
        [Fact]
        public async Task Book_AddBookWhenExists()
        {
            var bookId = 3;
            var bookRequest = new AddUpdateBookRequest()
            {
                Id=1,
                AuthorId=1,
                LastUpdated=DateTime.Now,
                Price=10,
                Quantity=10,
                Title="The Adventure Through Time"
            };
            _bookRepo.Setup(x => x.GetBookByName(bookRequest.Title)).ReturnsAsync(() => _books.FirstOrDefault(x => x.Title == bookRequest.Title));
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Add(bookRequest);
            //assert
            var badRequestObjResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjResult);
            var resultValue = badRequestObjResult.Value;
            Assert.NotNull(resultValue);
            Assert.Equal("Book Already Exists", resultValue);
        }
        [Fact]
        public async Task Update_Book_Ok()
        {
            //setup
            var bookId = 2;
            var newBook = new AddUpdateBookRequest()
            {
                Id = bookId,
                AuthorId = 10,
                LastUpdated = DateTime.Now,
                Price = 10,
                Quantity = 10,
                Title = "Avavasdsda"
            };
            var book = _books.FirstOrDefault(x => x.Id == bookId);

            _bookRepo.Setup(x => x.GetById(bookId)).ReturnsAsync(book);
            _bookRepo.Setup(x => x.UpdateBook(It.IsAny<Book>()))
                .Callback(() =>
                {
                    book.Title = "mnou qka kniga";
                })!.ReturnsAsync(() => book);
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Update(newBook);
            //assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);
            var resultValue = okObjResult.Value as Book;
            Assert.NotNull(resultValue);
            Assert.Equal("mnou qka kniga", resultValue.Title);
        }
        [Fact]
        public async Task Delete_Book_OK()
        {
            //setup
            var bookId = 1;
            var book = _books.FirstOrDefault(x => x.Id == bookId);
            _bookRepo.Setup(x => x.GetById(bookId)).ReturnsAsync(book);
            _bookRepo.Setup(x => x.DeleteBook(bookId))
                .Callback(() =>
                {
                    _books.Remove(book);
                }).ReturnsAsync(() => book);
            //inject
            var service = new BookService(_bookRepo.Object, _logger.Object, _authorRepo.Object);
            var controller = new BookController(service, _bookControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Delete(bookId);
            //assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);
            var resultValue = okObjResult.Value as Book;
            Assert.NotNull(resultValue);
            Assert.Equal(1, _books.Count);
            Assert.Equal(2, _books.First().Id);
            Assert.True(!_books.Contains(resultValue));
        }
    }
}
