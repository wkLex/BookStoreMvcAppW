using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Respositories.Abstracts;

namespace BookStoreMvcAppW.Respositories.Implementation
{
    public class GenreService : IGenreService // CRUD Operation
    {
        private readonly DatabaseContext ctx; //ctx context
        // create constructor
        public GenreService(DatabaseContext cxt)
        {
            this.ctx = cxt;
        }

        // Update the Add, Create Method
        public bool Create(Genre model)
        {
            try
            {
                ctx.Genres.Add(model);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }   

        }

        public bool Delete(int Id) //Delete, Remove mehtod
        {
            try
            {
                var data = this.GetByID(Id);
                if (data == null) // if data equals to null, then return false
                    return false;
                ctx.Genres.Remove(data);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre GetByID(int Id)
        {
            var data = ctx.Genres.Find(Id);
            return ctx.Genres.Find(Id);
        }

        public IQueryable<Genre> List()
        {
            var data = ctx.Genres.AsQueryable();
            return data;
        }

        public bool Update(Genre model) // Update Update method like Create method above
        {
            try
            {
                ctx.Genres.Update(model);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
