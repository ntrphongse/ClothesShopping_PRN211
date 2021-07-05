using DAOLibrary.DataAccess;
using DAOLibrary.Repository.Interface;
using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository.Object
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(string email) => UserDAO.Instance.GetUser(email);

        public User Login(string email, string password)
        {
            return UserDAO.Instance.Login(email, password);
        }
    }
}
