using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace BookStoreMvcAppW.Respositories.Implementation
{
    public class FileService : IFileService
    // Define readonly webhost
    // Define constructor
    // Iwebhostenvironment called environment or _environment?
    // Good practice:
    //You should use camelCase naming conventions for private fields
    //and use an underscore prefix to distinguish them from local variables
    // Follows the naming conventions for C#, where type names (such as class names and interface names)
    // are typically written in PascalCase


    // TEST USING IHOSTINGENVIRONMENT INSTEAD OF IWEBHOSTINGENVIRONEMNT
    //{
    //    private readonly IHostingEnvironment _environment;

    //    public FileService(IHostingEnvironment environment)
    //    {
    //        _environment = environment;
    //    }

    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment env) // Define constructor, pass IWebhost.. as a paratmeter
        {
            this._environment = env;
        }

        // Code for saving images
        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {

            try
            {
                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }

                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }

        }
        // Code for Delete images
        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
                if (System.IO.File.Exists(path)) // If that filename with path exists = we're deleting that file
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
