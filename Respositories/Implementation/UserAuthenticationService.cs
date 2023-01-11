using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Models.DTO;
using BookStoreMvcAppW.Respositories.Abstracts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace BookStoreMvcAppW.Respositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService // UserAS (implentation) Inherites from IUserAS (interface, abstract)

    // Implementing methods for Authetication, users and roles

    {
        // Declare our objects for User manager, role manager and sign in manaager
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        // Constructor
        public UserAuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;

        }
        // Methods
        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            // Finding our user by username
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)

                // If name taken, returns user alreday exist (in the database)
            {
                status.StatusCode = 0;
                status.Message = "User already exist";
                return status;
            }

            // Add later - ROLES AND CHANGE PASSWORD
            // If username not taken = create new user
            // All new data comming from frontend user input
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                EmailConfirmed = true, // Add method further on to verify this!!
                // PhoneNumberConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            // Define the roles
            if (!await roleManager.RoleExistsAsync(model.Role)) // If this roll doesnt exist already we will create it
                await roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }

        // Login check
        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Username); // user name doesn't exist, not find in database
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password)) // Password check
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

           // var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true); // Set to False - if first is set to true = true means using cookies, and we're not. Second True=logout on failer = true, yes.

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, true, true); // First "true" is set from false to true after implementation. Now both set to true. Will save in cookie now, like a "Remember me" functionality


            if (signInResult.Succeeded) // if sign in succeded, we will find the user roles
            {
                var userRoles = await userManager.GetRolesAsync(user); // Getting the user roles, finding them and
                var authClaims = new List<Claim>  // Adding the user roles to the claim list
                {
                    new Claim(ClaimTypes.Name, user.UserName),  // Creating a clame with Name and we're Assign username to this claim
                    };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole)); // Adding another claim: role.
                }
                status.StatusCode = 1; // Login worked, equals to false
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut) // If equals to TRUE = 0, sign in failed, user locked out
            {
                status.StatusCode = 0; 
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0; 
                status.Message = "Error on logging in";
            }

            return status; // returns one of the three status messages
        }

        public async Task LogoutAsync() // Log out mehtod 
        {
            await signInManager.SignOutAsync();

        }

        // DO THIS LATER !!!
        //public async Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        //{
        //    var status = new Status();

        //    var user = await userManager.FindByNameAsync(username);
        //    if (user == null)
        //    {
        //        status.Message = "User does not exist";
        //        status.StatusCode = 0;
        //        return status;
        //    }
        //    var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        status.Message = "Password has updated successfully";
        //        status.StatusCode = 1;
        //    }
        //    else
        //    {
        //        status.Message = "Some error occcured";
        //        status.StatusCode = 0;
        //    }
        //    return status;

        //}
    }
}
