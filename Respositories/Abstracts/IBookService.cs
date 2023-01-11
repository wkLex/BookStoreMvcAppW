using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Models.DTO;

namespace BookStoreMvcAppW.Respositories.Abstracts
{
    public interface IBookService // Crud operation
    {
        bool Create(Book model); // Create, add new Book and take Book model
        bool Update(Book model); // Update, edit book and take book model
        Book GetByID(int Id); // Will return the Book object by Id
        bool Delete(int Id); // Delete by Id
       BookListVm List(string term="", bool paging = false, int currentPage = 0); // A list of type Book, will return a list of Book objects
                                        // Parameters for searching and pagining (BookService.cs, Index.cshtml, HomeController.cs)
       List<int> GetGenreByBookId(int bookId); // MultiSelectList for genres, connected to BookService.cs and class Book.cs
    }
}
