using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models.Enums;

namespace App.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserLevel UserLevel { get; set; }

        public User(int id, string email, UserLevel userlevel)
        {
            Id = id;
            Email = email;
            UserLevel = userlevel;
        }
    }
}
