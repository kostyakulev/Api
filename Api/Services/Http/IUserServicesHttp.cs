namespace Api.Services.Http
{
    public interface IUserServicesHttp
    {
        Task<List<UserHttp>> GetAllUsers();
        Task<UserHttp?> GetSingleUser(int id);
        Task<UserHttp> AddUserAsync(UserHttp userhttp);
        Task<UserHttp>? UpdateUserAsync(int id, UserHttp userhttp);
        Task<UserHttp>? DeleteUserAsync(int id);
    }
}
