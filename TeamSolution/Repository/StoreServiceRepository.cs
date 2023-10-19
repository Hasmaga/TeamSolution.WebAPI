using Microsoft.EntityFrameworkCore;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class StoreServiceRepository : IStoreServiceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public StoreServiceRepository(ApplicationDbContext context, ILogger<StoreServiceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<StoreService> GetStoreServiceByIdRepositoryAsync(Guid id)
        {
            try
            {                
                return await _context.StoreServices.FirstOrDefaultAsync(x => x.Id == id);

            } 
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
