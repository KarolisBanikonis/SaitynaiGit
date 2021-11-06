using InfoMovies.Auth.Model;
using InfoMovies.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Entities
{
    public class Comment : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Required]
        public string UserId { get; set; }
        public InfoMoviesUser User { get; set; }
    }
}
