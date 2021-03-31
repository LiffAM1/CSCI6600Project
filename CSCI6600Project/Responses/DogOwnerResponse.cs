using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public class DogOwnerResponse : ResponseBase
    {
        public DogOwnerResponse(DogOwner model)
        {
            CopyProperties(model, this); 
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string CountryCode { get; set; }
    }
}
