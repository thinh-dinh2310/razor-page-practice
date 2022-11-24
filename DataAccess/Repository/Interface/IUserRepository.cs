using BusinessObject;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IUserRepository
    {
        User GetUserByEmailAndPassword(string email, string password);
        User GetUserById(Guid userId);
        User GetUserByEmail(string email);
        void RegisterUser(User user);
        Task<PaginationResult<User>> GetAllUsers(int offset, int limit, string keywords);
        User GetUserByIdAsync(Guid userId);
        void UpdateUserById(Guid userId, string email, string password, string firstName, string lastName, string displayName, string phone, DateTime dateOfBirth,
            string address, string description, byte[] resume, byte[] avatar);
        void DeleteUserById(Guid userId);
        void CreateUser(string userRole, string email, string password, string firstName, string lastName, string phone, string address, byte[] avatar);
        Task<User> UpdateUser(User user);
    }
}
