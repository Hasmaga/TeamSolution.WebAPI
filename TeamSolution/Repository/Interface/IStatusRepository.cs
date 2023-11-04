namespace TeamSolution.Repository.Interface
{
    public interface IStatusRepository
    {
        Task<Guid> FindIdByStatusNameAsync(string roleName);
        Task<String> GetStatusNameByStatusIdRepositoryAsync(Guid statusId);
    }
}
