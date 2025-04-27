using DataTable.Data;
using DataTable.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataTable.Service
{
    public class DataService : IDataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Generic method to get data for any table
        public async Task<List<T>> GetDataAsync<T>() where T : class
        {
            var dbSet = _context.Set<T>(); // Get DbSet dynamically
            return await dbSet.ToListAsync(); // Fetch all records for the table
        }
    }

}
