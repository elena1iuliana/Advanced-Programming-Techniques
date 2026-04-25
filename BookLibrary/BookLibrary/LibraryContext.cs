using Microsoft.EntityFrameworkCore;


public class LibraryContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Lab_Ultimate_Library;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 2, Title = "Foundation", Author = "Isaac Asimov", Genre = "SF", IsBorrowed = false },
            new Book { Id = 3, Title = "The Shining", Author = "Stephen King", Genre = "Horror", IsBorrowed = false },
            new Book { Id = 4, Title = "It", Author = "Stephen King", Genre = "Horror", IsBorrowed = false },
            new Book { Id = 5, Title = "Harry Potter", Author = "J.K. Rowling", Genre = "Fantasy", IsBorrowed = false },
            new Book { Id = 6, Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", IsBorrowed = false },
            new Book { Id = 7, Title = "1984", Author = "George Orwell", Genre = "Dystopian", IsBorrowed = false },
            new Book { Id = 8, Title = "Brave New World", Author = "Aldous Huxley", Genre = "Dystopian", IsBorrowed = false },
            new Book { Id = 9, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", IsBorrowed = false },
            new Book { Id = 10, Title = "Sherlock Holmes", Author = "Arthur Conan Doyle", Genre = "Mystery", IsBorrowed = false },
            new Book { Id = 11, Title = "Dracula", Author = "Bram Stoker", Genre = "Horror", IsBorrowed = false },
            new Book { Id = 12, Title = "The Witcher", Author = "Andrzej Sapkowski", Genre = "Fantasy", IsBorrowed = false },
            new Book { Id = 13, Title = "Metro 2033", Author = "Dmitry Glukhovsky", Genre = "SF", IsBorrowed = false },
            new Book { Id = 14, Title = "The Martian", Author = "Andy Weir", Genre = "SF", IsBorrowed = false },
            new Book { Id = 15, Title = "Neuromancer", Author = "William Gibson", Genre = "Cyberpunk", IsBorrowed = false }
        );
    }
}