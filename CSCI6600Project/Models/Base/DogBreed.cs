using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Base
{
    public class DogBreed
    {
        public DogBreed()
        {
            Dogs = new HashSet<Dog>();
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

        public BreedGroup Group { get; set; }
        public ICollection<Dog> Dogs { get; set; }
    }
}
