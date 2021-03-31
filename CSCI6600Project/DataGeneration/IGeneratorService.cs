using CSCI6600Project.Models;
using CSCI6600Project.Models.Base;
using CSCI6600Project.Models.Context;
using System.Collections.Generic;

namespace CSCI6600Project.DataGeneration
{
    public interface IGeneratorService 
    {
        List<DogOwner> GenerateOwners(int number = 1,bool save=false);
        List<Dog> GenerateDogs(int number = 1, bool save = false);
    }
}
