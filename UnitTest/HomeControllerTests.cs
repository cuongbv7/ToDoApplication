using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Controllers;
using TodoApplication.Data;
using TodoApplication.Models;
using TodoApplication.Models.ViewModels;

namespace UnitTest;

public class HomeControllerTests
{

    private DataContext dataContext;
    private HomeController homeController;

    [SetUp]
    public void Setup()
    {
        // Create an in-memory database for testing
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Initialize the DataContext with the in-memory database
        dataContext = new DataContext(options);

        // Create an instance of the HomeController with the test DataContext
        homeController = new HomeController(dataContext);
    }

    [TearDown]
    public void TearDown()
    {
        // Dispose the in-memory database after each test
        dataContext.Database.EnsureDeleted();
        dataContext.Dispose();
    }

    [Test]
    public async Task Index_ReturnsViewResultWithTodoViewModel()
    {
        // Arrange
        var todoItems = new List<TodoItem>()
            {
                new TodoItem { Id = 1, Summary = "Test Summary 1", Status = "To Do", Description = "Test Description 1", Assignee = "Test Assignee 1" },
                new TodoItem { Id = 2, Summary = "Test Summary 2", Status = "In Progress", Description = "Test Description 2", Assignee = "Test Assignee 2" }
            };
        await dataContext.TodoItems.AddRangeAsync(todoItems);
        await dataContext.SaveChangesAsync();

        // Act
        var result = await homeController.Index() as ViewResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<TodoViewModel>(result.Model);
        var viewModel = (TodoViewModel)result.Model;
        Assert.That(viewModel.TodoList.Count, Is.EqualTo(todoItems.Count));
    }
 
}
