using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public class DogSummaryResponse: ResponseBase
    {
        public DogSummaryResponse()
        {
        }

        public DogSummaryResponse(DogSummaryResponse other)
        {
            CopyProperties(other, this);
        }

        public DogSummaryResponse(Dog model)
        {
            CopyProperties(model, this, new List<string>() { "Breed" });
            Breed = model.Breed.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public string Breed { get; set; }
    }
}
