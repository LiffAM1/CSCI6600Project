using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public class DogBreedResponse : ResponseBase
    {
        public DogBreedResponse(DogBreed model)
        {
            CopyProperties(model, this, new List<string>() { "Group" });
            Group = new BreedGroupResponse(model.Group);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MinimumWeight { get; set; }
        public int MaximumWeight { get; set; }
        public int MinimumHeight { get; set; }
        public int MaximumHeight { get; set; }
        public int MinimumLifeExpectancy { get; set; }
        public int MaximumLifeExpectancy { get; set; }
        public string Description { get; set; }
        public int BreedPopularity { get; set; }
        public BreedGroupResponse Group { get; set; }
    }
}
