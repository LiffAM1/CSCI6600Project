using CSCI6600Project.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Context
{
    public interface IDatabaseContext 
    {
        DbSet<BreedGroup> BreedGroups { get; set; }
        DbSet<Dog> Dogs { get; set; }
        DbSet<DogBreed> DogBreeds { get; set; }
        DbSet<DogOwner> DogOwners { get; set; }

        void AddRange(IEnumerable<object> entities);
        int SaveChanges();
    }
}
