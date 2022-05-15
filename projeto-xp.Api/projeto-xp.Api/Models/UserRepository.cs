using Microsoft.EntityFrameworkCore;

namespace projeto_xp.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public async Task<List<UserItemCreate>> GetAllUserItems()
        {
            return await _context.UserItems.ToListAsync();
        }
        public Task<UserItemCreate> GetUserItemById(string id)
        {
            return _context.UserItems.FirstOrDefaultAsync(u => u.Id == id);
        }
        public Task AddUser(UserItemCreate userItem)
        {
            _context.UserItems.Add(userItem);
            return _context.SaveChangesAsync();
        }
        public Task UpdateUser(UserItemCreate userItem)
        {
            _context.Entry(userItem).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
        public Task DeleteUser(UserItemCreate userItem)
        {
            _context.Remove(userItem);
            return _context.SaveChangesAsync();
        }
        public bool Exists(string id)
        {
            return _context.UserItems.Any(e => e.Id == id);
        }
    }
}
