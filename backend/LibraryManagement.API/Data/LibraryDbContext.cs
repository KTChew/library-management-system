using LibraryManagement.API.Enums;
using LibraryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(
            DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
            .Property(book => book.Status)
            .HasConversion<string>();

            modelBuilder.Entity<Book>().HasData(
               new Book
               {
                   Id = 1,
                   Title = "Clean Code",
                   Author = "Robert C. Martin",
                   Status = BookStatus.Available
               },

               new Book
               {
                   Id = 2,
                   Title = "Domain-Driven Design",
                   Author = "Eric Evans",
                   Status = BookStatus.Borrowed
               },

               new Book
               {
                   Id = 3,
                   Title = "The Pragmatic Programmer",
                   Author = "Andy Hunt",
                   Status = BookStatus.Available
               }
           );
        }
    }
}