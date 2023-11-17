namespace Api.Services.UserServices
{
    public interface IUserServices
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetSingleUser(int id);
        Task<List<User>> AddUserAsync(User user);
        Task<List<User>?> UpdateUserAsync(int id, User user);
        Task<List<User>?> DeleteUserAsync(int id);
    }
}
