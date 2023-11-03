using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.DTO
{
    public class CreateUserDTO
    {
        public CreateUserDTO(string name, string email, string password, int age, int role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Age = age;
            this.Role = role;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int Role { get; set; }
    }
}
