using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public class BreedSummaryResponse : ResponseBase
    {
        public BreedSummaryResponse()
        {
        }

        public BreedSummaryResponse(BreedSummaryResponse other)
        {
            CopyProperties(other, this);
        }

        public BreedSummaryResponse(DogBreed model)
        {
            CopyProperties(model, this, new List<string>() { "Group" });
            Group = new BreedGroupResponse(model.Group);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BreedPopularity { get; set; }
        public BreedGroupResponse Group { get; set; }
    }
}
