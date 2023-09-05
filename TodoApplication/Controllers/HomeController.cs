using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using TodoApplication.Data;
using TodoApplication.Models;
using TodoApplication.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace TodoApplication.Controllers;

public class HomeController : Controller
{
    private readonly DataContext dataContext;

    public HomeController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    public async Task <IActionResult> Index()
    {
        var todoList = await dataContext.TodoItems.ToListAsync();
        return View(new TodoViewModel()
        {
            TodoList=todoList
        });
    }

    

}

