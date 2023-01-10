using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Models.DTO;
using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BookStoreMvcAppW.Respositories.Implementation
{
    public class BookService : IBookService // CRUD Operation
    {
        private readonly DatabaseContext ctx; //ctx context

        // create constructor
        public BookService(DatabaseContext cxt)
        {
            this.ctx = cxt;
        }
        // Update the Create Method (Add)

        public bool Create(Book model)

        {
            try
            {
                ctx.Books.Add(model);
               // ctx.Book.Add(model); //  Books! Book doesn't work
                ctx.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var bookGenre = new BookGenre
                    {
                        BookId = model.Id,
                        GenreId = genreId
                    };

                    ctx.BookGenres.Add(bookGenre); // Must be Add-method (not create)
                }

                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    public bool Delete(int Id) // Delete, Remove mehtod
        {
            try
            {
                var data = this.GetByID(Id);
                if (data == null) // if data equals to null, then return false
                    return false;
                var bookGenres = ctx.BookGenres.Where(a => a.BookId == data.Id); // bg = BookGenres
                foreach(var bookGenre in bookGenres)
                {
                    ctx.BookGenres.Remove(bookGenre);  // Remove the item from the list.
                                                       // From the BookGenres table in Db remove this bookgenre object by Id
                }
                ctx.Books.Remove(data);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Book GetByID(int Id)
        {
            var data = ctx.Books.Find(Id);
            return ctx.Books.Find(Id);
        }
        // Method in the for search by title plus paging on Home page
        // Connect the parameters, bool paging=false, int currentPage=0, with the interface IBookService definition
        public BookListVm List(string term="", bool paging=false, int currentPage=0) // Changed name from IQueryable<Book> to BookListVm
                                                                                    // Connection to this list in the DTO file->BookListVm.cs
        {
            var data = new BookListVm();
        
            var list = ctx.Books.ToList(); // show List of books from database table Books
            if (!string.IsNullOrEmpty(term))
            {
                term.ToLower();
                list = list.Where(a=>a.Title.ToLower().StartsWith(term)).ToList();
            }
            if (paging) // if paging is false, then
            {
                // we're applying paging
                int pageSize = 15; 
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
                data.PageSize= pageSize;
                data.CurrentPage= currentPage;
                data.TotalPages= TotalPages;
            }

            //list and jointQuery from the database tables
            foreach (var book in list) 
            {
                var genres = (from genre in ctx.Genres
                              join bg in ctx.BookGenres // bg bookgenre
                              on genre.Id equals bg.GenreId
                              where bg.BookId == book.Id
                              select genre.GenreName).ToList();
                var genreNames = string.Join(',', genres);
                book.GenreNames = genreNames;

            }
            data.BookList = list.AsQueryable();
            return data;
        }

        public bool Update(Book model) // Update method like Create method above
        {
            try
            {
                // Remove old genre, when updating with new genre(s)
                // These genreIds are not selected by users and are still present in bookGenres table
                // corresponding to this bookId. So these ids should be removed
                var genresToDeleted = ctx.BookGenres.Where(a => a.BookId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach(var bGenre in genresToDeleted)  // bGenre = bookGenre object
                {
                    ctx.BookGenres.Remove(bGenre); // this object will be removed from the BookGenres table in Db
                }
                foreach (int genId in model.Genres) // Solving: genre does not updating when editing
                {
                    var bookGenre = ctx.BookGenres.FirstOrDefault(a => a.BookId == model.Id && a.GenreId == genId);
                    if (bookGenre == null)  // If bookGenre equals to null, we will add this in the table
                    {
                        bookGenre = new BookGenre { GenreId = genId, BookId = model.Id };
                        ctx.BookGenres.Add(bookGenre);
                    }
                }
                ctx.Books.Update(model);
                // we have to add these genre Id:s in bookGenres table
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Create a multiple select list for genre Id:s for updating Books in admin
        // Mehtod (connected to new property, definition in class Book.cs)
        // Method also declared in interface IBookService.cs
        public List<int> GetGenreByBookId(int bookId)
        {
            var genreIds=ctx.BookGenres.Where(a=>a.BookId==bookId).Select(a=>a.GenreId).ToList();
            return genreIds;

        }
    }
}
