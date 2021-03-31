using System;
using System.Collections.Generic;

#nullable disable

namespace CSCI6600Project.Models.Index
{
    public partial class BreedGroup
    {
        public BreedGroup()
        {
            DogBreeds = new HashSet<DogBreed>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DogBreed> DogBreeds { get; set; }
    }
}
