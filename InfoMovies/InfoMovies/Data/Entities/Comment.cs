using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
