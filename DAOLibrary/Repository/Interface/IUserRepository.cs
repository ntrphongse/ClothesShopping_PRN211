using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLibrary.Repository.Interface
{
    public interface IUserRepository
    {
        public User Login(string email, string password);
        public User GetUser(string email);

        IEnumerable<User> GetUserList();
    }
}
