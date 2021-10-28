using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoMovies.Data.Dtos.Auth
{
    public class InfoMoviesUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string AdditionalInfo { get; set; }
    }
}
