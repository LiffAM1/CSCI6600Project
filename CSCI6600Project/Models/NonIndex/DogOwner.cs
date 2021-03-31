using CSCI6600Project.Models.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace CSCI6600Project.Models.NonIndex
{
    public partial class DogOwner : IDogOwner
    {
        public DogOwner()
        {
            Dogs = new HashSet<IDog>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<IDog> Dogs { get; set; }
    }
}
