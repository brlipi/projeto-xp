using System.Collections.Generic;

namespace projeto_xp.Models
{
    public interface IUserRepository
    {
        UserItemCreate GetUserItemById(string id);
        IEnumerable<UserItemCreate> GetAllUserItems();
        void AddUser(UserItemCreate userItem);
        //void UpdateUser(UserItemUpdate userItem);
        void SaveChanges();
    }
}
