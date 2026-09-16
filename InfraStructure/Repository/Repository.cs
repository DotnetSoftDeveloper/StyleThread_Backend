using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.IRepository;
using InfraStructure.Context;

namespace InfraStructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _table;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _table.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            return await _table.FindAsync(id, cancellationToken);
        }

        public async Task InsertAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public async Task UpdateAsync(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task DeleteAsync(object id, CancellationToken cancellationToken)
        {
            var existing = await GetByIdAsync(id, cancellationToken);
            if (existing != null)
            {
                _table.Remove(existing);
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
