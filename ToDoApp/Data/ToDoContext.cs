using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;  // Make sure this using directive is included

namespace ToDoApp.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
