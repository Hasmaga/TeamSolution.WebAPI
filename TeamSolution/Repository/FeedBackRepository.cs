using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FeedBackRepository(ApplicationDbContext context, ILogger<FeedBackRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateFeedbackRepositoryAsync(FeedBack feedBack)
        {
            try
            {
                _logger.LogInformation("CreateFeedbackRepositoryAsync");
                await _context.FeedBacks.AddAsync(feedBack);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Create Feedback At Repository: " + ex.ToString());
                throw;
            }
        }
    }
}
