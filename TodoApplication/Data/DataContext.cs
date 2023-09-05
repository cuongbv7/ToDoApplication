using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApplication.Models;

namespace TodoApplication.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}

