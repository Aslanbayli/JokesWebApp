using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IList<Joke> Jokes { get; set;}

        public ApplicationUser()
        {
            
        }
    }
}
    