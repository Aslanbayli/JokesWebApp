using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IJokeRepository Jokes { get; }
        Task<int> Complete();
    }
}
