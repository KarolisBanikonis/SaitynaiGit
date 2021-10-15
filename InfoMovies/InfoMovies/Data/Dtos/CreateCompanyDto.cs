using System.ComponentModel.DataAnnotations;

namespace InfoMovies.Data.Dtos
{
    public record CreateCompanyDto([Required]string Name, [Required] string Description);
}