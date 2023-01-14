using BookStoreMvcAppW.Models.DTO;
using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvcAppW.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService; // Name it auhtService
        // Method create a constructor
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService; // This as equals to as
        }

        // We will Create a user with admin rights. After that we will comment this method
        // Because we only need one user with those rights in this app
        // If you need more admin users, you can implement this registreation method with view
        //This method in IUserAuService.cs: // Task<Status> LoginAsync(LoginModel model); // is connected to the one down here


        //public async Task<IActionResult> Register() // Defined as a async method
        //{
        //    var model = new RegistrationModel
        //    {
        //        Email = "admin@gmail.com",
        //        Username = "admin",
        //        Name = "Admin",
        //        Password = "Admin@123",
        //        PasswordConfirm = "Admin@123",
        //        Role = "Admin",

        //    };

        // If you want to regiser with user, change Role = "User"

        // Use the authService functionality var result equals to our model
        //    var result = await authService.RegisterAsync(model); // Async method
        //    return Ok(result.Message);
        //}

        // Login method (get method??), login functionality
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // Post method, login result
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid) // If login NOT valid, return view model
              return View(model);
            var result = await authService.LoginAsync(model);
           
            if(result.StatusCode==1) //If Succesfully login -> Redirect to Homepage view ("Index") in controller "Home"
                return RedirectToAction("Index", "Home");
            
            else
            {
                TempData["msg"] = "Could not log in. Please, check the Username and Password.";
                return RedirectToAction(nameof(Login));
            }
          
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}
