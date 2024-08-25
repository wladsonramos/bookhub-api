using bookhub_api.Data;
using Microsoft.EntityFrameworkCore;

namespace bookhub_api.Books
{
    public static class BooksRoutes
    {
        public static void AddRoutesBooks(this WebApplication app)
        {
            var routesBooks = app.MapGroup("books");

            routesBooks.MapPost("", async (AddBookRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var isExist = await context.Books.AnyAsync(book => book.Name == request.Name, ct);

                if (isExist)
                    return Results.Conflict("Já existe um livro com esse nome!");
    
                var newBook = new Book(request.Name, request.Description);     
                await context.Books.AddAsync(newBook, ct);
                await context.SaveChangesAsync(ct);

                return Results.Ok(newBook);
            });

            routesBooks.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                var books = await context.Books.ToListAsync(ct);
                return books;
            });

            routesBooks.MapPut("{id}", async (Guid id, UpdateBookRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var book = await context.Books.SingleOrDefaultAsync(book => book.Id == id, ct);

                if (book == null)
                    return Results.NotFound();

                book.UpdateBook(request.Name, request.Description);

                await context.SaveChangesAsync(ct);
                return Results.Ok(book);
            });

            routesBooks.MapDelete("{id}", async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var book = await context.Books.SingleOrDefaultAsync(book => book.Id == id, ct);

                if (book == null)
                    return Results.NotFound();

                context.Books.Remove(book);
                await context.SaveChangesAsync(ct);
                return Results.NoContent();
            });
        }
    }
}
