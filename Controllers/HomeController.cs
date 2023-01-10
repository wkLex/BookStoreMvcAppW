using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvcAppW.Controllers
{  public class HomeController : Controller
    {
        private readonly IBookService _bookService; // Should be declared inside a class

        public HomeController(IBookService bookService) // pass it with an argument
        {
            _bookService = bookService;
        }

        //public IActionResult Index(string term = "", int currentPage = 1) // page Index.cshtml comment out atm
        //{
        //    // var books = _bookService.List(term, true, currentPage);
        //    var books = _bookService.List(term, true, currentPage);
        //    return View(books);
        //}
        public IActionResult Index(string term="", int currentPage = 1) // Search method on the Home page (Index.cshtml)
        {
            var books = _bookService.List(term, true, currentPage); // Pass term, asking for paging? = true (BookService, IBookService, Index.cshtml)
            return View(books); // pass the books here
        }

        public IActionResult About()
        {
            return View();
        }
        //public IActionResult BookDetail(int bookId) // Views->Home->BookDetail.cshtml
        //{
        //    var book = _bookService.GetByID(bookId);
        //    return View();
        //}

        public IActionResult BookDetail(int bookid) // Show book details when clicking on image, show on BookDetail page (from Index page)
        {
            var book = _bookService.GetByID(bookid);
            if (book == null) return NotFound(); 

            return View(book);
        }

    }
}
