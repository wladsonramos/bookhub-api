namespace bookhub_api.Books
{
    public class Book
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Book(string name, string description)
        {
            Name = name;
            Description = description;
            Id = Guid.NewGuid();
        }

        public void UpdateBook(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
