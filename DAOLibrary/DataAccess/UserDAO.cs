using DTOLibrary;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public User Login(string email, string password)
        {
            User loginUser = null;

            try
            {
                using var context = new lPVNgP26wKContext();
                loginUser = context.Users
                                .Include(u => u.RoleNavigation)
                                .SingleOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return loginUser;
        }

        public User GetUser(string email)
        {
            User user = null;
            try
            {
                using var context = new lPVNgP26wKContext();
                user = context.Users
                        .Include(u => u.RoleNavigation)
                        .SingleOrDefault(u => u.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public IEnumerable<User> GetUserList()
        {
            var users = new List<User>();
            try
            {
                using var context = new lPVNgP26wKContext();
                users = context.Users.Include(u => u.RoleNavigation).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public User GetUserById(int Id)
        {
            User user;
            try
            {
                using var context = new lPVNgP26wKContext();
                user = context.Users.Include(u => u.RoleNavigation).FirstOrDefault(u => u.UserId == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public void Update(User user)
        {
            try

            {
                using var context = new lPVNgP26wKContext();
                context.Users.Update(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SetAccountStatus(int id, bool statusVal)
        {
            try
            {
                using var context = new lPVNgP26wKContext();
                User user = GetUserById(id);
                user.Status = statusVal;
                context.Users.Update(user);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int userId)
        {
            try
            {
                User user = GetUserById(userId);
                if (user != null)
                {
                    using var context = new lPVNgP26wKContext();
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The product not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
