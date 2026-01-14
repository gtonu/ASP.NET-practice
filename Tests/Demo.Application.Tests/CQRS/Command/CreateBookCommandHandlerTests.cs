using Autofac.Extras.Moq;
using Demo.Application.CQRS.Command;
using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using Demo.Domain.UnitOfWork;
using MapsterMapper;
using Moq;
using Shouldly;

namespace Demo.Application.Tests;

public class CreateBookCommandHandlerTests
{
    private AutoMock _moq;
    private CreateBookCommandHandler _createBookCommandHandler;
    private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
    private Mock<IBookRepository> _bookRepositoryMock;
    private Mock<IMapper> _mapperMock;
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _moq = AutoMock.GetLoose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _moq?.Dispose();
    }

    [SetUp]
    public void Setup()
    {
        _applicationUnitOfWorkMock = _moq.Mock<IApplicationUnitOfWork>();
        _bookRepositoryMock = _moq.Mock<IBookRepository>();
        _mapperMock = _moq.Mock<IMapper>();
        _createBookCommandHandler = _moq.Create<CreateBookCommandHandler>();
    }

    [TearDown]
    public void TearDown()
    {
        _applicationUnitOfWorkMock?.Reset();
        _bookRepositoryMock?.Reset();

    }

    [Test]
    public async Task Handle_ValidBookName_CreatesBook()
    {
        //Arrange
        var command = new CreateBookCommand
        {
            Title = "UnitTest",
            Description = "This book covers fundamentals of Unit testing",
            Price = 10.50M,
            AuthorName = "X",
            InStock = 2
        };

        var book = new Book
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            AuthorName = command.AuthorName,
            InStock = command.InStock
        };

        //these _bookRepositoryMock.Setup() methods are same but applies different techniques for bypassing the AddAsync()
        //method.first one just passes object to check by creating the book object above and second one checks without creating
        //a book object..

        /* //technique 1..
        _bookRepositoryMock.Setup(x => x.AddAsync(
            book))
            .Returns(Task.CompletedTask)
            .Verifiable();
        */

        //technique 2..
        _bookRepositoryMock.Setup(x => x.AddAsync(
            It.Is<Book>(y =>
            y.Title == command.Title
            && y.Description == command.Description
            && y.Price == command.Price
            && y.AuthorName == command.AuthorName
            && y.InStock == command.InStock
            && y.Id != Guid.Empty
            ))).Returns(Task.CompletedTask)
            .Verifiable();

        _applicationUnitOfWorkMock.Setup(x => x.BookRepository)
                 .Returns(_bookRepositoryMock.Object)
                 .Verifiable();

        _mapperMock.Setup(x => x.Map<Book>(command)).Returns(book);
        //Act
        var result = await _createBookCommandHandler.Handle(command, CancellationToken.None);

        //Assert

        this.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => result.ShouldBeOfType<Book>(),
            () => result.Id.ShouldNotBe(Guid.Empty),
            () => result.Title.ShouldBe(command.Title),
            () => result.Description.ShouldBe(command.Description),
            () => result.Price.ShouldBe(command.Price),
            () => result.AuthorName.ShouldBe(command.AuthorName),
            () => result.InStock.ShouldBe(command.InStock),

            () => _bookRepositoryMock.VerifyAll(),
            () => _applicationUnitOfWorkMock.VerifyAll()
            );
    }

    [Test]
    public void Handle_TitleIsNull_ThrowsException()
    {
        //Arrange
        var command = new CreateBookCommand
        {
            Title = null
        };

        //Act & Assert
        Should.ThrowAsync<ArgumentNullException>(
           () => _createBookCommandHandler.Handle(command, CancellationToken.None));
    }
}
