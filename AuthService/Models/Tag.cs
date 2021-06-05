using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public String Name { get; set; }


        public Tag(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
