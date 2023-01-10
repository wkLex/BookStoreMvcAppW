using Microsoft.Build.Framework;

namespace BookStoreMvcAppW.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string? GenreName { get; set; }

    }
}
