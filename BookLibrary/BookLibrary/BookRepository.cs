using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context) => _context = context;

    public void Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void Update(Book book)
    {
        
        _context.SaveChanges();
    }

    public Book? GetById(int id) => _context.Books.Find(id);

   
    public IQueryable<Book> GetAllReadOnly() => _context.Books.AsNoTracking();
}