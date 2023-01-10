using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookStoreMvcAppW.Models.Domain
{
    public class Book
    {
        public int Id { get; set; } // pk

        [Required]
        public string? Title { get; set; } // ? makes it nullable string

        [Required]
        public string? Author { get; set; } // Can by a future FK 
        public string? ReleaseYear { get; set; }
     
        public string? BookImage { get; set; } // stores book image name with extension (ex, image0002.jpg)

        [NotMapped] // Not sent and showed in the Database table
       // [Required] // To prevent the server side to requier image and prevent uploading image /Troubleshooting ?
        public IFormFile? ImageFile { get; set; } // Adding question marks to get through in the web form asking "required" (here and down)
        [NotMapped]
        [Required] 
        public List<int>? Genres { get; set; } // changed from string Id to list of ints
        public IEnumerable<SelectListItem>? GenreList; //List Connected to Views -> Book ->Create 
                                                       // Define the list also in BookController.cs
        [NotMapped]
        public string ? GenreNames { get; set; }
        [NotMapped]
        public MultiSelectList ? MultiGenreList {get;set;} // Connected to BookController.cs Update Method, IBookService.se, BookService.se

    }
}
