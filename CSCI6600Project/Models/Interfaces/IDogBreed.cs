using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Models.Interfaces
{
    public interface IDogBreed
    {
        Guid Id { get; set; }
        string Name { get; set; }
        Guid GroupId { get; set; }
        int MinimumWeight { get; set; }
        int MaximumWeight { get; set; }
        int MinimumHeight { get; set; }
        int MaximumHeight { get; set; }
        int MinimumLifeExpectancy { get; set; }
        int MaximumLifeExpectancy { get; set; }
        string Description { get; set; }
        int BreedPopularity { get; set; }

        IBreedGroup Group { get; set; }
        ICollection<IDog> Dogs { get; set; }
    }
}
