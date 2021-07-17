using JokesWebApp.Data;
using JokesWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Repositories
{
    public class JokeRepository : GenericRepository<Joke>, IJokeRepository
    {

        public JokeRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Joke>> ShowSearchFrom(string SearchPhrase)
        {
            return await _context.Joke.Where(j => j.JokeQuestion.Contains(SearchPhrase)).ToListAsync();
        }

    }
}
