using CSCI6600Project.Models;
using CSCI6600Project.Models.Context;
using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using CSCI6600Project.Responses;

namespace CSCI6600Project.DataGeneration
{
    public interface IDataService
    {
        List<DogResponse> GetDogs(bool useIndex = false, bool useCache = false, Guid? id = null, string breed = null, Guid? breedId = null, string name = null, string ownerFirstName = null, string ownerLastName = null, Guid? ownerId = null, int? popularity = null, string countryCode=null);
        List<DogBreedResponse> GetBreeds(bool useIndex = false, bool useCache = false, Guid? id = null, string name = null, int? popularity = null, string group = null, Guid? groupId = null);
        List<DogOwnerResponse> GetOwners(bool useIndex = false, bool useCache = false, Guid? id = null, string firstName = null, string lastName = null, string countryCode=null, string dog = null, Guid? dogId = null, string breed = null);
    }
}
