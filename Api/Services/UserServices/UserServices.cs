namespace Api.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly TestDatabaseContext _context;

        public UserServices(TestDatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var singleUser = await _context.Users.FindAsync(id);
            if (singleUser == null)
                return false;

            _context.Users.Remove(singleUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var singleUser = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (singleUser == null)
                return null;
            return singleUser;


        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var singleUser = await _context.Users.FindAsync(id);
            if (singleUser == null)
                return null;

            singleUser.Username = user.Username;
            singleUser.Email = user.Email;

            await _context.SaveChangesAsync();

            return singleUser;
        }
    }

}

