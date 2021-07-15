using JokesWebApp.Models;
using System.Collections.Generic;

namespace JokesWebApp.Repositories
{
    public interface IJokeRepository : IGenericRepository<Joke>
    {
        IEnumerable<Joke> ShowSearchFrom(string SearchPhrase); 
    }
}
