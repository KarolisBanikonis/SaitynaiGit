using System.ComponentModel.DataAnnotations;

namespace InfoMovies.Data.Dtos
{
    public record CreateCommentDto([Required]string Text);
}
