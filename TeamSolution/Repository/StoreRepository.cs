using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using Microsoft.EntityFrameworkCore;

namespace TeamSolution.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Store> GetStoreByIdRepositoryAsync(Guid id)
        {
            try
            {
                return await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
