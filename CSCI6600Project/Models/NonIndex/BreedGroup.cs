using CSCI6600Project.Models.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace CSCI6600Project.Models.NonIndex
{
    public partial class BreedGroup : IBreedGroup
    {
        public BreedGroup()
        {
            DogBreeds = new HashSet<IDogBreed>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IDogBreed> DogBreeds { get; set; }
    }
}
