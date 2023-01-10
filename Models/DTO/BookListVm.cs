using BookStoreMvcAppW.Models.Domain;

namespace BookStoreMvcAppW.Models.DTO
{
    // Book List View Model, connection to IQueryable<Book> list
    // in BookService.cs (in Implementation folder)
    public class BookListVm 
    {
        public IQueryable<Book> BookList{get; set;}
        public int PageSize { get; set; } // pagination on Index View, Home page
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } 
        public string? Term { get; set; } // serach function Index, Home page
    }
}
