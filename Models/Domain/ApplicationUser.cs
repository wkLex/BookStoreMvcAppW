using Microsoft.AspNetCore.Identity;

namespace BookStoreMvcAppW.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

    }
}
