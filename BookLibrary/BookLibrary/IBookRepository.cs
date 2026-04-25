public interface IBookRepository
{
    void Add(Book book);
    void Update(Book book);
    Book? GetById(int id);
    IQueryable<Book> GetAllReadOnly();
}