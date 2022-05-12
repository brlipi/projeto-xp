using System.Collections.Generic;
using System.Linq;

namespace projeto_xp.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public UserItemCreate GetUserItemById(string id)
        {
            return _context.UserItems.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserItemCreate> GetAllUserItems()
        {
            return _context.UserItems;
        }

        public void AddUser(UserItemCreate userItem)
        {
            _context.UserItems.Add(userItem);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
