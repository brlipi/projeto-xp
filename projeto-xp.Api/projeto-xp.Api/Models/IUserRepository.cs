using System.Collections.Generic;

namespace projeto_xp.Api.Models
{
    public interface IUserRepository
    {
        Task<List<UserItemCreate>> GetAllUserItems();
        Task<UserItemCreate> GetUserItemById(string id);
        Task AddUser(UserItemCreate userItem);
        Task UpdateUser(UserItemCreate userItem);
        Task DeleteUser(UserItemCreate userItem);
        bool Exists(string id);
    }
}
