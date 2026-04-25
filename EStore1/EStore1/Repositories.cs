using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStore
{
    // Requirement: Repository pattern
    public interface IProductRepository
    {
        Task<List<Produs>> GetAllAsync();
        Task AddAsync(Produs p);
        Task<Produs?> GetByIdAsync(int id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly EStoreContext _context;
        public ProductRepository(EStoreContext context) => _context = context;

        public async Task<List<Produs>> GetAllAsync() => await _context.Produse.ToListAsync();
        public async Task<Produs?> GetByIdAsync(int id) => await _context.Produse.FindAsync(id);
        public async Task AddAsync(Produs p)
        {
            await _context.Produse.AddAsync(p);
            await _context.SaveChangesAsync();
        }
    }
}