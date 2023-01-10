namespace BookStoreMvcAppW.Respositories.Abstracts
{
    public interface IFileService
        // File for photos for book section.
        // Two Methods: 1. Save Images 2. Delete Images
        // Then implement this interface inside the class in Implementation folder
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
