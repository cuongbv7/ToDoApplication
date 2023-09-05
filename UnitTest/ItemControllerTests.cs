using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Controllers;
using TodoApplication.Data;
using TodoApplication.Models;
using TodoApplication.Models.ViewModels;

namespace UnitTest;

public class ItemControllerTests
{
    private DataContext dataContext;
    private ItemController itemController;

    [SetUp]
    public void Setup()
    {
        // Create an in-memory database for testing
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Initialize the DataContext with the in-memory database
        dataContext = new DataContext(options);

        // Create an instance of the ItemController with the test DataContext
        itemController = new ItemController(dataContext);
    }

    [TearDown]
    public void TearDown()
    {
        // Dispose the in-memory database after each test
        dataContext.Database.EnsureDeleted();
        dataContext.Dispose();
    }

    [Test]
    public async Task Add_ReturnsRedirectToActionResult()
    {
        // Arrange
        var todoViewModel = new TodoViewModel()
        {
            Todo = new TodoItem()
            {
                Id = 1,
                Summary = "Test Summary",
                Status = "To Do",
                Description = "Test Description",
                Assignee = "Test Assignee"
            }
        };

        // Act
        var result = await itemController.Add(todoViewModel) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.ActionName, Is.EqualTo("Index"));
        Assert.That(result.ControllerName, Is.EqualTo("Home"));
    }

    [Test]
    public async Task Edit_ReturnsRedirectToActionResult()
    {
        // Arrange
        var todoViewModel = new TodoViewModel()
        {
            Todo = new TodoItem()
            {
                Id = 1,
                Summary = "Test Summary",
                Status = "To Do",
                Description = "Test Description",
                Assignee = "Test Assignee"
            }
        };

        // Act
        var result = await itemController.Edit(todoViewModel) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.ActionName, Is.EqualTo("Index"));
        Assert.That(result.ControllerName, Is.EqualTo("Home"));
    }

    [Test]
    public async Task Delete_ReturnsRedirectToActionResult()
    {
        // Arrange
        var todoViewModel = new TodoViewModel()
        {
            Todo = new TodoItem()
            {
                Id = 1,
                Summary = "Test Summary",
                Status = "To Do",
                Description = "Test Description",
                Assignee = "Test Assignee"
            }
        };

        // Act
        var result = await itemController.Delete(todoViewModel) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.ActionName, Is.EqualTo("Index"));
        Assert.That(result.ControllerName, Is.EqualTo("Home"));
    }

}
