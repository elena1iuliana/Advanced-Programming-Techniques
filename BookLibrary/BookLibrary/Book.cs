using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public bool IsBorrowed { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int? BorrowedByUserId { get; set; }
}