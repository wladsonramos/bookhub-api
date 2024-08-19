using bookhub_api.Data;
using Microsoft.EntityFrameworkCore;

namespace bookhub_api.Books
{
    public static class BooksRoutes
    {
        public static void AddRoutesBooks(this WebApplication app)
        {
            var routesBooks = app.MapGroup("books");

            routesBooks.MapPost("", async (AddBookRequest request, AppDbContext context) =>
            {
                var isExist = await context.Books.AnyAsync(book => book.Name == request.Name);

                if (isExist)
                    return Results.Conflict("Já existe um livro com esse nome!");
    
                var newBook = new Book(request.Name, request.Description);     
                await context.Books.AddAsync(newBook);
                await context.SaveChangesAsync();

                return Results.Ok(newBook);
            });
        }
    }
}
