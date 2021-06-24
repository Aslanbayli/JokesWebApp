using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.ViewModels
{
    public class JokeCreateViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string JokeQuestion { get; set; }
        public string JokeAnswer { get; set; }
    }
}
