using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoRepository
{
    public class TodoDbContext : DbContext
    {

        public TodoDbContext(string cnnstr) : base(cnnstr)
        {
        }

        public IDbSet<TodoItem> TodoItems { get; set; }

        public IDbSet<TodoLabel> TodoItemLabels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(t => t.DateDue).IsOptional();
            modelBuilder.Entity<TodoItem>().Property(t => t.DateCompleted).IsOptional();
            modelBuilder.Entity<TodoItem>().Property(t => t.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.UserId).IsRequired();

            modelBuilder.Entity<TodoLabel>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoLabel>().Property(t => t.Value).IsRequired();
        }
    }
}
