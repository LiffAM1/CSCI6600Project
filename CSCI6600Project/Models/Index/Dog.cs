using CSCI6600Project.Models.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace CSCI6600Project.Models.Index
{
    public partial class Dog : IDog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid BreedId { get; set; }
        public Guid OwnerId { get; set; }

        public virtual IDogBreed Breed { get; set; }
        public virtual IDogOwner Owner { get; set; }
    }
}
