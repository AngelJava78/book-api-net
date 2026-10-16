using System.ComponentModel.DataAnnotations;

namespace BooksApiDemo.DTOs;

public class CreateBookRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Author { get; set; } = string.Empty;

    [Range(1, 9999)]
    public int PublicationYear { get; set; }
}
