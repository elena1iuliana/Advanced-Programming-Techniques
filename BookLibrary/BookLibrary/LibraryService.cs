using Microsoft.EntityFrameworkCore;

public class LibraryService
{
    private readonly LibraryContext _context;
    public LibraryService(LibraryContext context) => _context = context;

    public User GetOrCreateUser(string fn, string ln)
    {
        var user = _context.Users.FirstOrDefault(u => u.FirstName == fn && u.LastName == ln);
        if (user == null)
        {
            user = new User { FirstName = fn, LastName = ln };
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        return user;
    }

    public void AddBook(string t, string a, string g)
    {
        _context.Books.Add(new Book { Title = t, Author = a, Genre = g });
        _context.SaveChanges();
    }

    public bool BorrowBook(int bookId, int userId)
    {
        var book = _context.Books.Find(bookId);
        if (book == null || book.IsBorrowed) return false;
        book.IsBorrowed = true;
        book.BorrowedByUserId = userId;
        book.ReturnDate = DateTime.Now.AddDays(14);
        _context.SaveChanges();
        return true;
    }

 
    public List<Book> GetAllBooks() => _context.Books.AsNoTracking().OrderBy(b => b.Title).ToList();

    public List<Book> GetAvailableBooks() => _context.Books.AsNoTracking().Where(b => !b.IsBorrowed).ToList();

  
    public dynamic GetStatistics()
    {
        return _context.Books.AsNoTracking()
            .GroupBy(b => b.Genre)
            .Select(g => new { Genre = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();
    }

    public List<Book> Search(string term) =>
        _context.Books.AsNoTracking().Where(b => b.Title.Contains(term)).ToList();
}