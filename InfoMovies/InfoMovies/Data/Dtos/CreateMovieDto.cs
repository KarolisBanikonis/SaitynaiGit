using System.ComponentModel.DataAnnotations;

namespace InfoMovies.Data.Dtos
{
    public record CreateMovieDto([Required]string Title, [Required]string Story, [Required]double Score);
}
