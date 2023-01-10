using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace BookStoreMvcAppW.Controllers
{
   [Authorize] // Admin access 
    public class BookController : Controller
    {
        // Constructor
        private readonly IBookService _bookService; // underscore _ good practic to add
        private readonly IFileService _fileService; 
        private readonly IGenreService _genService; 

        public BookController(IBookService BookService, IFileService fileService, IGenreService genService) // No underscore here
        {
            _bookService = BookService; // _bookService equals to BookService
            _fileService = fileService;
            _genService = genService;
        }

        // Create, Add method connected to BookService.cs
        public IActionResult Create()
        {
            var model = new Book();
            //Get the list from the database
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost] // Post Method CREATE, add
        public IActionResult Create(Book model)
        {
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            if (!ModelState.IsValid) // Check if this model is valid or not, if not valid return view model
                return View(model);
            if (model.ImageFile != null) //if the imageFile doesn't equal to null, then run this code
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile); // save image
                if (fileResult.Item1 == 0) // if fileresult item1 equals to zero, then return an error message
                {
                    TempData["msg"] = "The file could not be saved";
                    return View(model);
                }
                var imageName = fileResult.Item2; //Otherwise imageName equals to fileResult.item2
                model.BookImage = imageName;
            }
            var result = _bookService.Create(model); // if result is succesfull, then return message
                                                      // TempData["msg"] = result ? "Successfully created" : "Could not save the information"; // if result is true will return the success-message, otherwise (:) retrun the second fail message
            if (result)
            {
                TempData["msg"] = "The new book was succesfully added and saved";
                return RedirectToAction(nameof(Create));
            }

            else // if Not succesfully created, return view
            {
                TempData["msg"] = "Sorry, the information could not be saved. An error occurred."; //Error on server side
                return View();
            }
        }

        // Update, Edit method connected to BookService.cs implementation

        public IActionResult Update(int Id)
        {
            var model = _bookService.GetByID(Id); //Find and pass this data to view
           // model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            var selectedGenres = _bookService.GetGenreByBookId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
           // model.MultiGenreList = multiGenreList;
           model.MultiGenreList = multiGenreList;
           return View(model);
        }

        [HttpPost] // UPDATE, Add
        public IActionResult Update(Book model)
        {
            var selectedGenres = _bookService.GetGenreByBookId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;
            if (!ModelState.IsValid)
                return View(model);
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            if (!ModelState.IsValid) // Check if this model is valid or not, if not valid return view model
                return View(model);
            if (model.ImageFile != null) //if the imageFile doesn't equal to null, then run this code
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile); // save image
                if (fileResult.Item1 == 0) // if fileresult item1 equals to zero, then return an error message
                {
                    TempData["msg"] = "The file could not be saved";
                    return View(model);
                }
                var imageName = fileResult.Item2; //Otherwise imageName equals to fileResult.item2
                model.BookImage = imageName;
            }
            var result = _bookService.Update(model);
            if (result)
            {
                TempData["msg"] = "The new information was succesfully saved";
                return RedirectToAction(nameof(BookList)); // IF successfully updated = back to the Book list
            }
            else // if Not succesfully created, return view
            {
                TempData["msg"] = "Error. Sorry, the information could not be saved"; //Error on server side
                return View(model);
            }

        }
        public IActionResult BookList()
        {
            var data = this._bookService.List();
            return View(data);
           // return Ok(data); // Not returning a view, used for debugging, checking the data
        }

        // Delete, remove method connected to BookService.cs
        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id);
            return RedirectToAction(nameof(BookList));
        }


    }
}
