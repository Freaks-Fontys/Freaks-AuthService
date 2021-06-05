using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Car Car { get; set; }
        //public Tag[] Preferences { get; set; }
        public String AvatarURL { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime LatestLoginAt { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
