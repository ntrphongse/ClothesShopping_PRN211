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
                lock(instanceLock)
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
            } catch (Exception ex)
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
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
