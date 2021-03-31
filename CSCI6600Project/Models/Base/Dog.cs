using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Base
{
    public partial class Dog 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid BreedId { get; set; }
        public Guid OwnerId { get; set; }

        public DogBreed Breed { get; set; }
        public DogOwner Owner { get; set; }
    }
}
