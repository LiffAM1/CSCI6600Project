using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public partial class BreedGroupResponse : ResponseBase
    {
        public BreedGroupResponse()
        {
        }

        public BreedGroupResponse(BreedGroupResponse other)
        {
            CopyProperties(other, this);
        }

        public BreedGroupResponse(BreedGroup model)
        {
            CopyProperties(model, this);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
