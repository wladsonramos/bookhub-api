namespace bookhub_api.Books
{
    public static class BooksRoutes
    {
        public static void AddRoutesBooks(this WebApplication app)
        {
            app.MapGet("books", () => new Book("Book 1", "Book 1 Description"));
        }
    }
}
