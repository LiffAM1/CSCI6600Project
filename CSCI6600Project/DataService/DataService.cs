using CSCI6600Project.Models;
using CSCI6600Project.Models.Context;
using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CSCI6600Project.Cache;
using CSCI6600Project.Responses;

namespace CSCI6600Project.DataGeneration
{
    public class DataService : IDataService
    {

        IDatabaseContext _nonIndexedDbContext;
        IDatabaseContext _indexedContext;

        IConfiguration _configuration;
        ICacheManager _cache;

        public DataService(IConfiguration config, ICacheManager cache, csci6600Context dbContext, csci6600_indexedContext dbContextIndexed)
        {
            _nonIndexedDbContext = dbContext;
            _indexedContext = dbContextIndexed;
            _configuration = config;
            _cache = cache;
        }
        public List<DogResponse> GetDogs(bool devNull = false, bool useIndex = false, bool useCache = false, Guid? id = null, string breed = null, Guid? breedId = null, string group = null, Guid? groupId = null, string name = null, string ownerFirstName = null, string ownerLastName = null, Guid? ownerId = null, int? popularity = null, string countryCode=null)
        {
            var db = useIndex ? _indexedContext : _nonIndexedDbContext;
            var parameters = new { id, breed, breedId, group, groupId, name, ownerFirstName, ownerLastName, ownerId, popularity, countryCode };
            var parameterList = new List<string>();
            foreach (PropertyInfo pi in parameters.GetType().GetProperties())
            {
                var val = pi.GetValue(parameters);
                if (val != null)
                {
                    parameterList.Add($"{pi.Name}:{val.ToString()}");
                }
            }
            if (useCache)
            {
                var cachedDogs = _cache.GetCacheValue<List<DogResponse>>(GenerateKey("Dog", parameterList));
                if (cachedDogs != null)
                    return !devNull ? cachedDogs : null;
             }
            var dogs = db.Dogs.Where(d =>
                ((id.HasValue ? (d.Id == id.Value) : true) &&
                (breedId.HasValue ? (d.BreedId == breedId.Value) : true) &&
                (ownerId.HasValue ? (d.OwnerId == ownerId.Value) : true) &&
                (!String.IsNullOrEmpty(name) ? (d.Name == name) : true) &&
                (!String.IsNullOrEmpty(breed) ? (d.Breed.Name == breed) : true) &&
                (!String.IsNullOrEmpty(ownerFirstName) ? (d.Owner.FirstName == ownerFirstName) : true) &&
                (!String.IsNullOrEmpty(ownerLastName) ? (d.Owner.LastName == ownerLastName) : true) &&
                (!String.IsNullOrEmpty(countryCode) ? (d.Owner.CountryCode == countryCode) : true) &&
                (popularity.HasValue ? (d.Breed.BreedPopularity == popularity.Value) : true) &&
                (groupId.HasValue ? (d.Breed.GroupId == groupId.Value) : true) &&
                (!String.IsNullOrEmpty(group) ? (d.Breed.Group.Name == group) : true)))
                .Include(d => d.Breed)
                .Include(d => d.Breed.Group)
                .Include(d => d.Owner)
                .Select(d => new DogResponse(d)).ToList();
            if (useCache || !devNull)
            {
                if (dogs.Count > 0 && useCache)
                    _cache.WriteToCache(GenerateKey("Dog", parameterList), dogs);
                if (!devNull)
                    return dogs;
            }
            return null;
        }

        public List<DogBreedResponse> GetBreeds(bool devNull = false, bool useIndex = false, bool useCache = false, Guid? id = null, string name = null, int? popularity = null, string group = null, Guid? groupId=null)
        {
            var db = useIndex ? _indexedContext : _nonIndexedDbContext;
            var parameters = new { id, name, popularity, group, groupId};
            var parameterList = new List<string>();
            foreach (PropertyInfo pi in parameters.GetType().GetProperties())
            {
                var val = pi.GetValue(parameters);
                if (val != null)
                {
                    parameterList.Add($"{pi.Name}:{val.ToString()}");
                }
            }
            if (useCache)
            {
                var cachedBreeds = _cache.GetCacheValue<List<DogBreedResponse>>(GenerateKey("DogBreed", parameterList));
                if (cachedBreeds != null)
                    return !devNull ? cachedBreeds : null;
             }
            var breeds = _indexedContext.DogBreeds.Where(b =>
                (id.HasValue ? (b.Id == id.Value) : true) &&
                (!String.IsNullOrEmpty(name) ? (b.Name == name) : true) &&
                (popularity.HasValue ? (b.BreedPopularity == popularity.Value) : true) &&
                (groupId.HasValue ? (b.GroupId == groupId.Value) : true) &&
                (!String.IsNullOrEmpty(group) ? (b.Group.Name == group) : true))
                .Include(d => d.Group)
                .Select(b => new DogBreedResponse(b)).ToList();
            if (useCache || !devNull)
            {
                if (breeds.Count > 0 && useCache)
                    _cache.WriteToCache(GenerateKey("DogBreed", parameterList), breeds);
                if (!devNull)
                    return breeds;
            }
            return null;
        }

        public List<DogOwnerResponse> GetOwners(bool devNull = false, bool useIndex = false, bool useCache = false, Guid? id = null, string firstName = null, string lastName = null, string countryCode=null, string dog=null, Guid? dogId = null, string breed = null)
        {
            var db = useIndex ? _indexedContext : _nonIndexedDbContext;
            var parameters = new { id, firstName, lastName, countryCode, dog, dogId, breed};
            var parameterList = new List<string>();
            foreach (PropertyInfo pi in parameters.GetType().GetProperties())
            {
                var val = pi.GetValue(parameters);
                if (val != null)
                {
                    parameterList.Add($"{pi.Name}:{val.ToString()}");
                }
            }
            if (useCache)
            {
                var dogOwners = _cache.GetCacheValue<List<DogOwnerResponse>>(GenerateKey("DogOwner", parameterList));
                if (dogOwners != null)
                    return !devNull ? dogOwners : null;
             }
            var owners = _indexedContext.DogOwners.Where(o =>
                (id.HasValue ? (o.Id == id.Value) : true) &&
                (!String.IsNullOrEmpty(firstName) ? (o.FirstName == firstName) : true) &&
                (!String.IsNullOrEmpty(lastName) ? (o.LastName == lastName) : true) &&
                (!String.IsNullOrEmpty(countryCode) ? (o.CountryCode == countryCode) : true) &&
                (!String.IsNullOrEmpty(dog) ? (o.Dogs.Any(d => d.Name == dog)) : true) &&
                (dogId.HasValue ? (o.Dogs.Any(d => d.Id == dogId)) : true) &&
                (!String.IsNullOrEmpty(breed) ? (o.Dogs.Any(d => d.Breed.Name == breed)) : true))
                .Include(o => o.Dogs)
                .ThenInclude(d => d.Breed)
                .Select(o => new DogOwnerResponse(o)).ToList();
            if (useCache || !devNull)
            {
                if (owners.Count > 0 && useCache)
                    _cache.WriteToCache(GenerateKey("DogOwner", parameterList), owners);
                if (!devNull)
                    return owners;
            }
            return null;
        }

        private string GenerateKey(string objectType, List<string> parameterValues)
        {
            return $"{objectType}-{String.Join(";", parameterValues)}";
        }

    }
}
