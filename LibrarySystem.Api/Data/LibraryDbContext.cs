using Microsoft.EntityFrameworkCore;
using LibrarySystem.Api.Entities;

namespace LibrarySystem.Api.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book { BookNumber = 1, Title = "A Pál utcai fiúk", Author = "Molnár Ferenc", Publisher = "Móra", PublicationYear = 1907 },
                new Book { BookNumber = 2, Title = "Egri csillagok", Author = "Gárdonyi Géza", Publisher = "Dante", PublicationYear = 1901 },
                new Book { BookNumber = 3, Title = "Clean Code", Author = "Robert C. Martin", Publisher = "Prentice Hall", PublicationYear = 2008 }
            );

            modelBuilder.Entity<Reader>().HasData(
                new Reader { ReaderNumber = 1, Name = "Tóth Tibor", Address = "4400 Nyíregyháza, Kossuth tér 1-3.", DateOfBirth = new DateTime(1990, 4, 20) },
                new Reader { ReaderNumber = 2, Name = "Takács Éva", Address = "4024 Debrecen, Piac utca 20.", DateOfBirth = new DateTime(2005, 9, 11) }
            );

        }
    }
}