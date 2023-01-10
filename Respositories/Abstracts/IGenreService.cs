using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Models.DTO;

namespace BookStoreMvcAppW.Respositories.Abstracts
{
    public interface IGenreService // Crud operation
    {
        bool Create(Genre model); // Create, add new Genre
        bool Update(Genre model); // Update, edit Genre
        Genre GetByID(int Id); // Will return the Genre object by Id
        bool Delete(int Id); // Delete by Id
        IQueryable<Genre> List(); // A list of type Genre, will return a list of genre objects
    }
}
