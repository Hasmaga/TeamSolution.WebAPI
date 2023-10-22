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
        public async Task<Guid> UpdateAsync(UpdateStoreRequestModel updateStoreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Update store with ID:" + updateStoreRequest.Id);
                var entity = _context.Stores.FindAsync(new object[] { updateStoreRequest.Id }, cancellationToken).Result;
                if (entity == null)
                {
                    throw new Exception("Thấy cái nịt");
                }
                _mapper.Map(updateStoreRequest.StoreModel,entity);
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}
