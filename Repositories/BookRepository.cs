using BooksApiDemo.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BooksApiDemo.Repositories;

public class BookRepository : IBookRepository
{
    private readonly string _connectionString;

    public BookRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BooksDatabase")
            ?? throw new InvalidOperationException("Connection string 'BooksDatabase' was not found.");
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        const string sql = """
            SELECT Id, Title, Author, PublicationYear
            FROM dbo.Books
            ORDER BY Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Book>(sql);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        const string sql = """
            SELECT Id, Title, Author, PublicationYear
            FROM dbo.Books
            WHERE Id = @Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Book>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Book book)
    {
        const string sql = """
            INSERT INTO dbo.Books (Title, Author, PublicationYear)
            OUTPUT INSERTED.Id
            VALUES (@Title, @Author, @PublicationYear);
            """;

        await using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(sql, book);
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        const string sql = """
            UPDATE dbo.Books
            SET Title = @Title,
                Author = @Author,
                PublicationYear = @PublicationYear
            WHERE Id = @Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        var affectedRows = await connection.ExecuteAsync(sql, book);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = """
            DELETE FROM dbo.Books
            WHERE Id = @Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }
}
