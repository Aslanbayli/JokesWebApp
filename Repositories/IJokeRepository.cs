using JokesWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokesWebApp.Repositories
{
    public interface IJokeRepository : IGenericRepository<Joke>
    {
        Task<IEnumerable<Joke>> ShowSearchFrom(string SearchPhrase); 
    }
}
