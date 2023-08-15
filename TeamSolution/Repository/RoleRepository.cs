using AutoMapper;
using TeamSolution.DAO.Interface;
using TeamSolution.Model;
using TeamSolution.Model.Dto;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class RoleRepository : IRoleReposotory
    {
        private readonly IRoleDAO _dao;
        private readonly ILogger _logger;
        private IMapper _mapper;
        public RoleRepository(IRoleDAO dao, ILogger<RoleRepository> logger, IMapper mapper)
        {
            _dao = dao;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> CreateRoleAsync(NewRoleReqDto role)
        {
            try
            {
                _logger.LogInformation("CreateRoleAsync: "+ role.RoleName);
                return await _dao.CreateRoleDAOAsync(_mapper.Map<Role>(role));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
