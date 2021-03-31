using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Interfaces
{
    public interface IDog
    {
        Guid Id { get; set; }
        string Name { get; set; }
        int Age { get; set; }
        Guid BreedId { get; set; }
        Guid OwnerId { get; set; }

        IDogBreed Breed { get; set; }
        IDogOwner Owner { get; set; }
    }
}
