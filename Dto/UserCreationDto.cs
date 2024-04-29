using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loginsystem.Dto
{
    public class UserCreationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}