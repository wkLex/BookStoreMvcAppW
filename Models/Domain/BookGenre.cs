namespace BookStoreMvcAppW.Models.Domain
{
    public class BookGenre // Many to many relationship, a book can have many genres, a genre can have many books
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}
