using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using Microsoft.EntityFrameworkCore;
using TeamSolution.ViewModel.Store;
using TeamSolution.Enum;
using AutoMapper;

namespace TeamSolution.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private IMapper _mapper;

        public StoreRepository(ApplicationDbContext context, ILogger<StoreRepository> logger,IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
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
                    queryable = queryable.Where(s => s.DeleteDateTime == null);
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
            try
            {
                _logger.LogInformation("Create store with ID:" + store.Id);
                await _context.Stores.AddAsync(store);
                await _context.SaveChangesAsync();
                return store.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> UpdateAsync(Store store, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Update store with ID:" + store.Id);
                var entity = await _context.Stores.FindAsync(new object[] { store.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }

                if (!string.IsNullOrEmpty(entity.StoreName))
                {
                    entity.StoreName = store.StoreName;
                }
                if (!string.IsNullOrEmpty(entity.Address))
                {
                    entity.Address = store.Address;
                }
                if (!string.IsNullOrEmpty(entity.StoreDescription))
                {
                    entity.StoreDescription = store.StoreDescription;
                }
                if (!string.IsNullOrEmpty(entity.StoreImage))
                {
                    entity.StoreImage = store.StoreImage;
                }
                if (!string.IsNullOrEmpty(entity.OperationTime))
                {
                    entity.OperationTime = store.OperationTime;
                }
                entity.UpdateDateTime = store.UpdateDateTime;

                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> DeleteAsync(Store store, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Delete store with ID:" + store.Id);
                var entity = await _context.Stores.FindAsync(new object[] { store.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }

                entity.DeleteDateTime = store.DeleteDateTime;

                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<Store>> GetFilterStoreByStoreNameRepositoryAsync(string storeName)
        {
            try
            {   // if storeName is null or empty, return all store
                _logger.LogInformation("GetFilterStoreByStoreNameRepositoryAsync: " + storeName);
                if (string.IsNullOrEmpty(storeName))
                {
                    return await _context.Stores.ToListAsync();
                }
                else
                {
                    // return list store with storeName have character contain storeName or like storeName
                    return await _context.Stores.Where(x => x.StoreName.ToLower().Contains(storeName.ToLower())).ToListAsync();                    
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
