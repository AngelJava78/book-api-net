using BooksApiDemo.Models;

namespace BooksApiDemo.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<int> CreateAsync(Book book);
    Task<bool> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
}
