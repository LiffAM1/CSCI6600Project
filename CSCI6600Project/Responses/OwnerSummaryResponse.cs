using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public partial class OwnerSummaryResponse : ResponseBase
    {
        public OwnerSummaryResponse()
        {
        }

        public OwnerSummaryResponse(OwnerSummaryResponse other)
        {
            CopyProperties(other, this);
        }

        public OwnerSummaryResponse(DogOwner model)
        {
            CopyProperties(model, this);
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
    }
}
