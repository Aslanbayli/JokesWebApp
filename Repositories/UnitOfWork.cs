using JokesWebApp.Data;
using JokesWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;
        public IJokeRepository Jokes { get; }

        public UnitOfWork(ApplicationDbContext applicationDbContext, IJokeRepository jokesRepository)
        {
            this._context = applicationDbContext;
            this.Jokes = jokesRepository;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) { 
                _context.Dispose();
            }
        }
    }
}
