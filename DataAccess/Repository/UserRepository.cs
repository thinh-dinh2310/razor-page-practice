using BusinessObject;
using BusinessObject.DTO;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByEmailAndPassword(string email, string password)
        {
            return UsersDAO.Instance.GetUserByEmailAndPassword(email, password);
        }
        public User GetUserById(Guid userId)
        {
           return UsersDAO.Instance.GetUserByIdAsync(userId);
        }
        public User GetUserByEmail(string email)
        {
            return UsersDAO.Instance.GetUserByEmail(email);
        }
        public void RegisterUser(User user)

            => UsersDAO.Instance.CreateUser(user.RoleId, user.Email, user.Password, user.FirstName, user.LastName, user.Phone, user.Address, user.Avatar);
        public Task<PaginationResult<User>> GetAllUsers(int offset, int limit, string keywords) => UsersDAO.Instance.GetAllUsers(offset, limit, keywords);
        public User GetUserByIdAsync(Guid userId) => UsersDAO.Instance.GetUserByIdAsync(userId);

        public void UpdateUserById(Guid userId, string email, string password, string firstName, string lastName, string displayName, string phone, DateTime dateOfBirth,
            string address, string description, byte[] resume, byte[] avatar)
            => UsersDAO.Instance.UpdateUserById(userId, email, password, firstName, lastName, displayName, phone, dateOfBirth, address, description, resume, avatar);
        public void DeleteUserById(Guid userId) => UsersDAO.Instance.DeleteUserById(userId);
        public void CreateUser(string userRole, string email, string password, string firstName, string lastName, string phone, string address, byte[] avatar)
            => UsersDAO.Instance.CreateUser(Guid.Parse(userRole), email, password, firstName, lastName, phone, address, avatar);
        public async Task<User> UpdateUser(User user)
        {
            return await UsersDAO.Instance.UpdateUser(user);
        }

    }
}
