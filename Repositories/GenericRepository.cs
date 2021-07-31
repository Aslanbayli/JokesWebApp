using JokesWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public DbContext _context;
        public DbSet<T> table;

        public GenericRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        // Interface Implementations
        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
