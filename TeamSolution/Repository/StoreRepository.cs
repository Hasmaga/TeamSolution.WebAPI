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
        public async Task<ICollection<Store>> GetAll(bool includeIsDeleted)
        {
            try
            {
                var queryable = _context.Stores.AsNoTracking();
                if(!includeIsDeleted)
                {
                    queryable = queryable.Where(_ => _.IsDelete == includeIsDeleted);
                }
                ICollection<Store> list = await queryable.ToListAsync();
                return list;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<Guid> CreateAsync(Store store, CancellationToken cancellationToken)
        {
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
            return Task.FromResult(store.Id).Result;
        }
    }
}
