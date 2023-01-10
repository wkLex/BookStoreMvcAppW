using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BookStoreMvcAppW.Controllers
{
   [Authorize] // These parts are authorized = Admin role
    public class GenreController : Controller
    {
        // Constructor
        private readonly IGenreService _genreService; // underscore _ good practic to add

        public GenreController(IGenreService GenreService)
        {
            _genreService = GenreService; // _genreService equals to genreService
        }

        // Create, Add method connected to GenreService.cs
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post Method
        public IActionResult Create(Genre model)
        {
            if (!ModelState.IsValid) // Check if this model is valid or not, if not valid return view model
                return View(model);
            var result = _genreService.Create(model); // if result is succesfull, then return message
                                                      // TempData["msg"] = result ? "Successfully created" : "Could not save the information"; // if result is true will return the success-message, otherwise (:) retrun the second fail message
            if (result)
            {

                TempData["msg"] = "The new genre was added and saved";
                return RedirectToAction(nameof(Create));
            }

            else // if Not succesfully created, return view
            {
                TempData["msg"] = "An error occurrd. Sorry, the information could not be saved."; //Error on server side
                return View();
            }
        }


        // Update, Edit method connected to GenreService.cs
        
        public IActionResult Update(int Id)
        {
            var data = _genreService.GetByID(Id); //Find and pass this data to view
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)

        {
            if (!ModelState.IsValid)

                return View(model);
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "The new information was saved";
                return RedirectToAction(nameof(GenreList)); // IF successfully updated = back to the genre list
            }

            else // if Not succesfully created, return view
            {
                TempData["msg"] = "Error. Sorry, the information could not be saved"; //Error on server side
                return View(model);
            }

        }
        public IActionResult GenreList()
        {
            var data = this._genreService.List().ToList();
            return View(data);
        }

        // Delete, remove method connected to GenreService.cs
        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }


    }
}
