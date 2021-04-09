using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public class DogResponse : ResponseBase
    {
        public DogResponse()
        {
        }

        public DogResponse(DogResponse other)
        {
            CopyProperties(other, this);
        }

        public DogResponse(Dog model)
        {
            CopyProperties(model, this, new List<string>() { "Breed", "Owner" });
            Breed = new BreedSummaryResponse(model.Breed);
            Owner = new OwnerSummaryResponse(model.Owner);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public BreedSummaryResponse Breed { get; set; }
        public OwnerSummaryResponse Owner { get; set; }
    }
}
