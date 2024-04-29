using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loginsystem.Models;

namespace Loginsystem.Interfaces
{
    public interface IUserInterface
    {
        public bool CreateUser(User user);
        public bool UserExitsts(Guid id);
        public bool LoginUser(string email, string password);
        public bool FindUserByEmail(string email);
    }
}