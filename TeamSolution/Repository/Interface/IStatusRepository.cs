namespace TeamSolution.Repository.Interface
{
    public interface IStatusRepository
    {
        Task<Guid> FindIdByStatusNameAsync(string roleName);
    }
}
