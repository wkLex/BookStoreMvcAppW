using BookStoreMvcAppW.Models.DTO;

namespace BookStoreMvcAppW.Respositories.Abstracts
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);

       // Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username); // Make later
    }
}
