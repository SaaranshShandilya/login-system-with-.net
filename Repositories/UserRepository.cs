using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loginsystem.Data;
using Loginsystem.Interfaces;
using Loginsystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace Loginsystem.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        private bool Save()
        {
            var res  = _context.SaveChanges();
            return res>0?true:false;
        }

        public bool FindUserByEmail(string email)
        {
            return _context.Users.Any(u=>u.Email == email);
        }

        public bool LoginUser(string email, string password)
        {
            var res = _context.Users.Where(u=>u.Email == email).FirstOrDefault();
            byte[] resPw = Convert.FromHexString(res.Password);
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);
            byte[] compareHashValue = SHA256.HashData(messageBytes);
            bool same = resPw.SequenceEqual(compareHashValue);
            return same;
        }

        public bool UserExitsts(Guid id)
        {
            return _context.Users.Any(c=>c.Id == id);
        }
    }
}