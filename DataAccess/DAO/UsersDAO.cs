using BusinessObject;
using BusinessObject.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UsersDAO
    {
        private static UsersDAO instance = null;
        private static readonly object instanceLock = new object();
        private UsersDAO() { }

        public static UsersDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UsersDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<PaginationResult<User>> GetAllUsers(int offset = 0, int limit = 10, string keywords = "")
        {
            PaginationResult<User> response = new PaginationResult<User>();
            List<User> list = new List<User>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                list = await context.Users
                        .Where(k => k.Email.ToLower().Contains(keywords))
                        .OrderByDescending(o => o.CreatedAt)
                        .Skip(offset * limit)
                        .Take(limit)
                        .ToListAsync();
                response.limit = limit;
                response.offset = offset;
                response.totalInPage = list.Count();
                response.totalItems = context.Users.Where(k => k.Email.ToLower().Contains(keywords)).Count();
                response.data = list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllUsers: " + ex.Message);
            }
            return response;
        }

        public User GetUserByIdAsync(Guid userId)
        {
            User tmp = null;
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = context.Users
                                    .Include(u => u.Role)
                                    .Include(s => s.UserSkills)
                                        .ThenInclude(s => s.Skills)
                                    .FirstOrDefault(m => m.Id == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetUserByIdAsync: " + ex.Message);
            }
            return tmp;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            User tmp = null;
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp =  context.Users
                                    .Include(u => u.Role)
                                    .FirstOrDefault(m => m.Email.ToLower() == email.ToLower()
                                                                && m.Password == password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetUserByEmailAndPassword: " + ex.Message);
            }
            return tmp;
        }

        public User GetUserByEmail(string email)
        {
            User tmp = null;
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp =  context.Users
                                    .Include(u => u.Role)
                                    .FirstOrDefault(m => m.Email.ToLower() == email.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetUserByEmail: " + ex.Message);
            }
            return tmp;
        }

        public async void UpdateUserById(Guid userId, string email, string password, string firstName, string lastName, string displayName, string phone,
            DateTime dateOfBirth, string address, string description, byte[] resume, byte[] avatar)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                User user = context.Users.FirstOrDefault(u => u.Id == userId);
                user.Email = email.Trim();
                user.Password = password.Trim();
                user.FirstName = firstName.Trim();
                user.LastName = lastName.Trim();
                user.DisplayName = displayName.Trim();
                user.Phone = phone.Trim();
                user.DateOfBirth = dateOfBirth;
                user.Address = address.Trim();
                user.Description = description.Trim();
                user.Resume = resume;
                user.Avatar = avatar;
                user.UpdatedAt = DateTime.Now;
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdateUserById: " + ex.Message);
            }
        }

        public async Task<User> UpdateUser(User userUpdate)
        {
            User tmp = new User();
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userUpdate.Id);
                if (userUpdate.Avatar == null)
                {
                    userUpdate.Avatar = tmp.Avatar;
                }

                if (userUpdate.Resume == null)
                {
                    userUpdate.Resume = tmp.Resume;
                }

                context.Update(userUpdate);
                await context.SaveChangesAsync();
                return userUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdateUser: " + ex.Message);
                return tmp;
            }
        }

        public async void DeleteUserById(Guid userId)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                User user = context.Users.Include(u => u.UserSkills).FirstOrDefault(u => u.Id.Equals(userId));
                foreach(var item in user.UserSkills)
                {
                    context.UserSkills.Remove(item);
                    await context.SaveChangesAsync();
                }
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at DeleteUserById: " + ex.Message);
            }
        }

        public async void CreateUser(Guid userRoleId, string email, string password, string firstName, string lastName, string phone, string address, byte[] avatar)
        {
            try
            {
                //string role = "";
                //if (userRole.Equals("USER", StringComparison.OrdinalIgnoreCase))
                //{
                //    role = USER_ROLE;
                //}
                //if (userRole.Equals("HR", StringComparison.OrdinalIgnoreCase))
                //{
                //    role = HR_ROLE;
                //}
                //if (userRole.Equals("INTERVIEWER", StringComparison.OrdinalIgnoreCase))
                //{
                //    role = INTERVIEWER_ROLE;
                //}

                User user = new User()
                {
                    Id = Guid.NewGuid(),
                    RoleId = userRoleId,
                    Email = email.Trim(),
                    Password = password.Trim(),
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    Phone = phone.Trim(),
                    Address = address.Trim(),
                    CreatedAt = DateTime.Now,
                    Avatar = avatar,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };
                var context = new eRecruitment_PRN221Context();
                await context.Users.AddAsync(user);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created user successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreateUser: " + ex.Message);
            }
        }
    }
}
