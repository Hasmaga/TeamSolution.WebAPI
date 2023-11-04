using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace TeamSolution.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public StatusRepository(
            ApplicationDbContext context, 
            ILogger<StatusRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> FindIdByStatusNameAsync(string statusName)
        {
            try
            {
                _logger.LogInformation("(StatusRepository)FindIdByStatusNameAsync: " + statusName);
                var status = await _context.Statuses.FirstOrDefaultAsync(x => x.StatusName == statusName);
                if (status != null)
                {
                    return status.Id;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError("(StatusRepository)Error find status: " + statusName + ex);
                throw;
            }
        }

        public async Task<String?> GetStatusNameByStatusIdRepositoryAsync(Guid statusId)
        {
            try
            {
                _logger.LogInformation($"GetStatusNameByStatusIdRepositoryAsync: {statusId}");
                var status = await _context.Statuses.FirstOrDefaultAsync(x => x.Id == statusId);
                if (status != null)
                {
                    return status.StatusName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
