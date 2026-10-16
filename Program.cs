using BooksApiDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
