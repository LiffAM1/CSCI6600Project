using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Interfaces
{
    public interface IBreedGroup
    {
        Guid Id { get; set; }
        string Name { get; set; }

        ICollection<IDogBreed> DogBreeds { get; set; }
    }
}
