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

namespace CSCI6600Project.DataGeneration
{
    public class DataService : IDataService
    {

        IDatabaseContext _nonIndexedDbContext;
        IDatabaseContext _indexedContext;

        IConfiguration _configuration;
        ICacheManager _cache;

        public DataService(IConfiguration config, ICacheManager cache, IDatabaseContext dbContext, IDatabaseContext dbContextIndexed)
        {
            _nonIndexedDbContext = dbContext;
            _indexedContext = dbContextIndexed;
            _configuration = config;
            _cache = cache;
        }
        public List<Dog> GetDogs(bool useIndex = false, bool useCache = false, Guid? id = null, string breed = null, Guid? breedId = null, string name = null, string ownerFirstName = null, string ownerLastName = null, Guid? ownerId = null, int? popularity = null)
        {
            var db = useIndex ? _indexedContext : _nonIndexedDbContext;
            var dogs = new List<Dog>();
            var parameters = new { id, breed, breedId, name, ownerFirstName, ownerLastName, ownerId, popularity };
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
                var cachedDogIds = _cache.GetCacheValue(GenerateKey("Dog", parameterList));
                if (cachedDogIds != null)
                    return db.Dogs.Where(d =>
                        cachedDogIds.Contains(d.Id))
                        .Include(d => d.Breed)
                        .Include(d => d.Breed.Group)
                        .Include(d => d.Owner)
                        .ToList();
             }
            dogs = db.Dogs.Where(d =>
                ((id.HasValue ? (d.Id == id.Value) : true) &&
                (!String.IsNullOrEmpty(breed) ? (d.Breed.Name == breed) : true) &&
                (breedId.HasValue ? (d.BreedId == breedId.Value) : true) &&
                (!String.IsNullOrEmpty(name) ? (d.Name == name) : true) &&
                (!String.IsNullOrEmpty(ownerFirstName) ? (d.Owner.FirstName == ownerFirstName) : true) &&
                (!String.IsNullOrEmpty(ownerLastName) ? (d.Owner.LastName == ownerLastName) : true) &&
                (ownerId.HasValue ? (d.OwnerId == ownerId.Value) : true ) &&
                (popularity.HasValue ? (d.Breed.BreedPopularity == popularity.Value) : true)))
                .Include(d => d.Breed)
                .Include(d => d.Breed.Group)
                .Include(d => d.Owner)
                .ToList();
            if (dogs.Count > 0 && useCache)
                _cache.WriteToCache(GenerateKey("Dog", parameterList), dogs.Select(d => d.Id).ToList());
            return dogs;
        }

        public List<DogBreed> GetBreeds(bool useIndex = false, bool useCache = false, Guid? id = null, string name = null, int? popularity = null, string group = null, Guid? groupId=null)
        {
            // if (useCache) and id is provided, try to get it from cache
            var breeds = new List<DogBreed>();
            if (useIndex)
            {
                breeds = _indexedContext.DogBreeds.Where(b =>
                    id.HasValue ? (b.Id == id.Value) : true &&
                    !String.IsNullOrEmpty(name) ? (b.Name == name) : true &&
                    popularity.HasValue ? (b.BreedPopularity == popularity.Value) : true &&
                    !String.IsNullOrEmpty(group) ? (b.Group.Name == group) : true &&
                    groupId.HasValue ? (b.GroupId == groupId.Value) : true)
                    .Include(d => d.Group)
                    .ToList();
            }
            else
            {
                breeds = _nonIndexedDbContext.DogBreeds.Where(b =>
                    id.HasValue ? (b.Id == id.Value) : true &&
                    !String.IsNullOrEmpty(name) ? (b.Name == name) : true &&
                    popularity.HasValue ? (b.BreedPopularity == popularity.Value) : true &&
                    !String.IsNullOrEmpty(group) ? (b.Group.Name == group) : true &&
                    groupId.HasValue ? (b.GroupId == groupId.Value) : true)
                    .Include(d => d.Group)
                    .ToList();
            }
            // if (isCache) and it wasn't found in cache, save it to cache
            return breeds;

        }

        public List<DogOwner> GetOwners(bool useIndex = false, bool useCache = false, Guid? id = null, string firstName = null, string lastName = null, string dog=null, Guid? dogId = null, string breed = null)
        {
            var owners = new List<DogOwner>();
            if (useIndex)
            {
                owners = _indexedContext.DogOwners.Where(o =>
                    id.HasValue ? (o.Id == id.Value) : true &&
                    !String.IsNullOrEmpty(firstName) ? (o.FirstName == firstName) : true &&
                    !String.IsNullOrEmpty(lastName) ? (o.LastName == lastName) : true &&
                    !String.IsNullOrEmpty(dog) ? (o.Dogs.Any(d => d.Name == dog)) : true &&
                    dogId.HasValue ? (o.Dogs.Any(d => d.Id == dogId)) : true &&
                    !String.IsNullOrEmpty(breed) ? (o.Dogs.Any(d => d.Breed.Name == breed)) : true)
                    .ToList();
            }
            else
            {
                owners = _nonIndexedDbContext.DogOwners.Where(o =>
                    id.HasValue ? (o.Id == id.Value) : true &&
                    !String.IsNullOrEmpty(firstName) ? (o.FirstName == firstName) : true &&
                    !String.IsNullOrEmpty(lastName) ? (o.LastName == lastName) : true &&
                    !String.IsNullOrEmpty(dog) ? (o.Dogs.Any(d => d.Name == dog)) : true &&
                    dogId.HasValue ? (o.Dogs.Any(d => d.Id == dogId)) : true &&
                    !String.IsNullOrEmpty(breed) ? (o.Dogs.Any(d => d.Breed.Name == breed)) : true)
                    .ToList();
            }
            // if (isCache) and it wasn't found in cache, save it to cache
            return owners;

        }

        private string GenerateKey(string objectType, List<string> parameterValues)
        {
            return $"{objectType}-{String.Join(";", parameterValues)}";
        }

    }
}
