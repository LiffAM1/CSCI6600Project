using CSCI6600Project.Models.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace CSCI6600Project.Models.NonIndex
{
    public partial class DogBreed : IDogBreed
    {
        public DogBreed()
        {
            Dogs = new HashSet<IDog>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GroupId { get; set; }
        public int MinimumWeight { get; set; }
        public int MaximumWeight { get; set; }
        public int MinimumHeight { get; set; }
        public int MaximumHeight { get; set; }
        public int MinimumLifeExpectancy { get; set; }
        public int MaximumLifeExpectancy { get; set; }
        public string Description { get; set; }
        public int BreedPopularity { get; set; }

        public virtual IBreedGroup Group { get; set; }
        public virtual ICollection<IDog> Dogs { get; set; }
    }
}
