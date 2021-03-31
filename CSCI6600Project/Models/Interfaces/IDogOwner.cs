using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Interfaces
{
    public interface IDogOwner
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        int Age { get; set; }
        string CountryCode { get; set; }

        ICollection<IDog> Dogs { get; set; }
    }
}
