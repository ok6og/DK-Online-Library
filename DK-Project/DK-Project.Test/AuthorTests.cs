using AutoMapper;
using BookStore.BL.Services;
using Castle.Core.Logging;
using DK_Project.AutoMapper;
using DK_Project.Controllers;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using DK_Project.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;

namespace DK_Project.Test
{
    public class AuthorTests
    {
        private readonly IList<Author> _authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Age = 69,
                DateOfBirth = DateTime.Now,
                Name = "Paisi",
                Nickname = "Boyang"
            },
            new Author()
            {
                Id = 2,
                Age = 9,
                DateOfBirth = DateTime.Now,
                Name = "Valeri",
                Nickname = "Valeri Suhiq"
            }
        };
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AuthorService>> _logger;
        private readonly Mock<IAuthorRepository> _authorRepoMock;
        private readonly Mock<IBookRepository> _bookRepoMock;
        private readonly Mock<ILogger<AuthorController>> _authorControllerLoggerMock;
        //AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger, IBookRepository bookRepository)
        public AuthorTests()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();
            _logger = new Mock<ILogger<AuthorService>>();
            _authorRepoMock = new Mock<IAuthorRepository>();
            _bookRepoMock = new Mock<IBookRepository>();
            _authorControllerLoggerMock = new Mock<ILogger<AuthorController>>();
        }


        [Fact]
        public async Task Author_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;
            _authorRepoMock.Setup(x => x.GetAllUsers())
                .ReturnsAsync(_authors);
            

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);

            //act
            var result = await controller.Get();
            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;

            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
            Assert.Equal(expectedCount, authors.Count());
            Assert.Equal(authors, _authors);
        }

        [Fact]
        public async Task Author_GetAll_RightAuthors()
        {
            //setup
            _authorRepoMock.Setup(x => x.GetAllUsers())
                .ReturnsAsync(_authors);

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);

            //act
            var result = await controller.Get();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;

            Assert.True(authors.Any());
            Assert.True(authors.LastOrDefault() == _authors.LastOrDefault());
            Assert.True(authors.FirstOrDefault() == _authors.FirstOrDefault());
        }
        [Fact]
        public async Task Author_GetAuthorById_Ok()
        {
            //setup
            var authorId = 1;
            var expectedAuthor = _authors.First(x => x.Id == authorId);


            _authorRepoMock.Setup(x=> x.GetById(authorId))
                .ReturnsAsync(_authors.First(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.GetByID(authorId);
            //Assert
            var okObjResult = result as OkObjectResult;
            Assert.NotNull(okObjResult);

            var author = okObjResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }
        [Fact]
        public async Task Author_NotFound_GetById()
        {
            //setup
            var authorId = 3;
            //var expectedAuthor = _authors.First(x => x.Id == authorId);


            _authorRepoMock.Setup(x=> x.GetById(authorId))
                .ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.GetByID(authorId);
            //Assert
            var notFoundObjResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjResult);

            var returnedAuthor = notFoundObjResult.Value;
            Assert.NotNull(returnedAuthor);
            Assert.Equal("Book Doesn't Exists",returnedAuthor);
        }
        [Fact]
        public async Task AddAuthorOk()
        {
            //setup
            var authorId = 3;
            var authorRequest = new AddUpdateAuthorRequest()
            {
                Nickname = "Valeri",
                Age = 22,
                DateOfBirth = DateTime.Now,
                Name = "Test Petur"
            };

            _authorRepoMock.Setup(x => x.AddUser(It.IsAny<Author>()))
                .Callback(() =>
            {
                var author1 =new Author()
                {
                    Id = 3,
                    Name = authorRequest.Name,
                    Nickname = authorRequest.Nickname,
                    Age = authorRequest.Age,
                    DateOfBirth = authorRequest.DateOfBirth
                };
                _authors.Add(author1);
            }).ReturnsAsync(()=> _authors.FirstOrDefault(x=>x.Id == authorId));

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Add(authorRequest);
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Author;
            Assert.NotNull(resultValue);
            Assert.Equal(3, resultValue.Id);
        }

        [Fact]
        public async Task Author_AddAuthorWhenExist()
        {
            //setup
            var authorId = 3;
            
            var authorRequest = new AddUpdateAuthorRequest()
            {
                Nickname = "Valeri",
                Age = 22,
                DateOfBirth = DateTime.Now,
                Name = "Valeri"
            };

            _authorRepoMock.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(()=>_authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Add(authorRequest);
            //assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);

            var resultValue = badRequestObjectResult.Value;
            Assert.NotNull(resultValue);
            Assert.Equal("Author Already Exists", resultValue);

        }
        [Fact]
        public async Task Update_Author_Ok()
        {
            //setup
            var authorId = 2;
            var newAuthor = new AddUpdateAuthorRequest()
            {
                Id= authorId,
                Nickname = "Simeon",
                Age = 32,
                DateOfBirth = DateTime.Now,
                Name = "AVram"
            };
            var author = _authors.FirstOrDefault(x => x.Id == authorId);

            _authorRepoMock.Setup(x => x.GetById(authorId)).ReturnsAsync(author);
            _authorRepoMock.Setup(x => x.UpdateUser(It.IsAny<Author>()))
                .Callback(() =>
                {
                    author.Nickname = "Gosho";
                })!.ReturnsAsync(() => author);

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Update(newAuthor);
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var resultValue = okObjectResult.Value as Author;
            Assert.NotNull(resultValue);
            Assert.Equal("Gosho", resultValue.Nickname);
        }
        [Fact]
        public async Task Delete_Author_OK()
        {
            //setup
            var authorId = 1;
            var author = _authors.FirstOrDefault(x => x.Id == authorId);
            _authorRepoMock.Setup(x=>x.GetById(authorId)).ReturnsAsync(author);

            _authorRepoMock.Setup(x => x.DeleteUser(authorId))
                .Callback(() =>
                {
                    _authors.Remove(author);
                }).ReturnsAsync(()=>author);

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(service, _authorControllerLoggerMock.Object, _mapper);
            //act
            var result = await controller.Delete(authorId);
            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as Author;
            Assert.NotNull(resultValue);
            Assert.Equal(1,_authors.Count);
            Assert.Equal(2, _authors.First().Id);

        }          
    }
}