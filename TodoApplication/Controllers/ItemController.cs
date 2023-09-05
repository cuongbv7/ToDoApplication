using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using TodoApplication.Data;
using TodoApplication.Models;
using TodoApplication.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using NUnit.Framework.Interfaces;


namespace TodoApplication.Controllers
{
	public class ItemController :Controller
	{
        private readonly DataContext dataContext;

        public ItemController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            
            return View();
        }

      

        [HttpPost]
        public async Task<IActionResult> Add(TodoViewModel todoViewModel)
        {
            var todoItem = new TodoItem()
            {
                Id = todoViewModel.Todo.Id,
                Summary = todoViewModel.Todo.Summary,
                Status = todoViewModel.Todo.Status,
                Description =todoViewModel.Todo.Description,
                Assignee = todoViewModel.Todo.Assignee
               
            };
            await dataContext.TodoItems.AddAsync(todoItem);
            await dataContext.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todoItem = await dataContext.TodoItems.FirstOrDefaultAsync(item => item.Id == id);
            if (todoItem != null)
            {
                return View(new TodoViewModel()
                {
                    Todo=todoItem
                    
                });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoViewModel todoViewModel)
        {
            var todoItem = await dataContext.TodoItems.FindAsync(todoViewModel.Todo.Id);
            if (todoItem != null)
            {
                todoItem.Assignee = todoViewModel.Todo.Assignee;
                todoItem.Description = todoViewModel.Todo.Description;
                todoItem.Summary = todoViewModel.Todo.Summary;
                todoItem.Status = todoViewModel.Todo.Status;
                await dataContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TodoViewModel todoViewModel)
        {
            var todoItem = await dataContext.TodoItems.FindAsync(todoViewModel.Todo.Id);
            if (todoItem != null)
            {
                dataContext.TodoItems.Remove(todoItem);
                await dataContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index", "Home");
        }


    }
}

