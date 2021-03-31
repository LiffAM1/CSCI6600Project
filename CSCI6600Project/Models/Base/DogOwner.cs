using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Base
{
    public class DogOwner
    {
        public DogOwner()
        {
            Dogs = new HashSet<Dog>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string CountryCode { get; set; }

        public ICollection<Dog> Dogs { get; set; }
    }
}
