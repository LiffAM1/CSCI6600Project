using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Base
{
    public partial class BreedGroup
    {
        public BreedGroup()
        {
            DogBreeds = new HashSet<DogBreed>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<DogBreed> DogBreeds { get; set; }
    }
}
