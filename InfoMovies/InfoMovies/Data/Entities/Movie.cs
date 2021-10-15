using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Story { get; set; }

        public double Score { get; set; }

        public DateTime CreationTimeUtc { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
