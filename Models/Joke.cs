using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Models
{
    public class Joke
    {
        public int ID { get; set; }

        public string JokeQuestion { get; set; }

        public string JokeAnswer { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Joke()
        {

        }
    }
}
