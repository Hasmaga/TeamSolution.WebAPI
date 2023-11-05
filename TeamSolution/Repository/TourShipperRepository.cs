using Microsoft.EntityFrameworkCore;
using TeamSolution.DatabaseContext;
using TeamSolution.Enum;
using TeamSolution.Helper;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class TourShipperRepository : ITourShipperRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public TourShipperRepository(ApplicationDbContext context, ILogger<TourShipperRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TourShipper?> GetTourByTourIdRepositoryAsync(Guid tourId)
        {
            _logger.LogInformation($"Get Tour by TourId: {tourId}");
            try
            {
                var Tour = await _context.TourShippers.FindAsync(tourId);
                if (Tour != null)
                {
                    return Tour;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at get Tour by TourId repository: {ex}");
                throw;
            }
        }
        public async Task<TourShipper?> GetTourByTourIdIncludeOrderRepositoryAsync(Guid tourId)
        {
            _logger.LogInformation($"Get Tour by TourId: {tourId}");
            try
            {
                var Tour = await _context.TourShippers.Where(t => t.Id == tourId).Include(s => s.TourGetOrders).Include(s=> s.TourShipOrders).FirstOrDefaultAsync();
                if (Tour != null)
                {
                    return Tour;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at get Tour by TourId repository: {ex}");
                throw;
            }
        }
        public async Task<ICollection<TourShipper>> GetAllToursRepositoryAsync(bool includeIsDeleted)
        {
            try
            {
                _logger.LogInformation("Get all Tours");
                var queryable = _context.TourShippers.AsNoTracking();
                if (!includeIsDeleted)
                {
                    queryable = queryable.Where(o => o.DeleteDateTime == null);
                }
                ICollection<TourShipper> list = await queryable.ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CreateTourRepositoryAsync(TourShipper tour)
        {
            try
            {
                _logger.LogInformation("CreateTourRepositoryAsync");
                await _context.TourShippers.AddAsync(tour);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Create Tour At Repository: " + ex.ToString());
                throw;
            }
        }
        public async Task<Guid> UpdateTourRepositoryAsync(TourShipper tour, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Update Tour with ID:" + tour.Id);
                var entity = await _context.TourShippers.FindAsync(new object[] { tour.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }

                if(tour.ShipperId != CoreHelper.DefaultGuid)
                {
                    entity.ShipperId = tour.ShipperId;
                }
                if (!string.IsNullOrEmpty(tour.DeliverOrGet))
                {
                    entity.DeliverOrGet = tour.DeliverOrGet.Trim();
                }
                
                entity.UpdateDateTime = tour.UpdateDateTime;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> UpdateTourStatusRepositoryAsync(TourShipper tour, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Update Tour status with ID:" + tour.Id);
                var entity = await _context.TourShippers.FindAsync(new object[] { tour.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }

                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }

                if(tour.StatusId != CoreHelper.DefaultGuid)
                {
                    entity.StatusId = tour.StatusId;
                }

                entity.UpdateDateTime = tour.UpdateDateTime;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> DeleteTourRepositoryAsync(TourShipper tour, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Delete Tour with ID:" + tour.Id);
                var entity = await _context.TourShippers.FindAsync(new object[] { tour.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }
                entity.DeleteDateTime = tour.DeleteDateTime;
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
