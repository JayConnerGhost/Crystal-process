using System;
using System.Collections.Generic;

namespace CrystalProcess.Models
{
    public class User
    {
        public User()
        {
            Roles = new List<CustomRole>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<CustomRole> Roles { get; set; }
    }
}
