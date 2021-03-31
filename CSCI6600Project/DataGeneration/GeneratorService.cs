using CSCI6600Project.Models;
using CSCI6600Project.Models.NonIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CSCI6600Project.DataGeneration
{
    public class GeneratorService : IGeneratorService
    {
        private List<string> _firstNames = new List<string>();
        private List<string> _lastNames = new List<string>();
        private List<string> _dogNames = new List<string>();
        private List<string> _countryCodes = new List<string>();

        private static List<string> _emailDomains = new List<string>()
        {
            "gmail.com",
            "hotmail.com",
            "students.ecu.edu",
            "yahoo.com",
            "aol.com"
        };

        private List<int> popularities = new List<int>();
        private Dictionary<int, int> popularityCounts = new Dictionary<int, int>();
        private Dictionary<int, List<DogBreed>> availableBreeds = new Dictionary<int, List<DogBreed>>();

        private csci6600Context _dbContext;

        public GeneratorService(csci6600Context dbContext)
        {
            _dbContext = dbContext;

            // Fill in the lists
            InitializeData();
        }

        private void InitializeData()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataGeneration\Data");
            
            // First Names
            var reader = new StreamReader(File.OpenRead(Path.Combine(path,"FirstNames.csv")));
            while (!reader.EndOfStream)
                _firstNames.AddRange(reader.ReadLine().Trim().Split(","));

            // Last Names
            reader = new StreamReader(File.OpenRead(Path.Combine(path,"LastNames.csv")));
            while (!reader.EndOfStream)
                _lastNames.AddRange(reader.ReadLine().Trim().Split(","));

            // Dog Names
            reader = new StreamReader(File.OpenRead(Path.Combine(path,"DogNames.csv")));
            while (!reader.EndOfStream)
                _dogNames.AddRange(reader.ReadLine().Trim().Split(","));

            // Country Codes
            reader = new StreamReader(File.OpenRead(Path.Combine(path,"CountryCodes.csv")));
            while (!reader.EndOfStream)
                _countryCodes.AddRange(reader.ReadLine().Trim().Split(","));

            // Create regression for popularities, which will be used to assure that more higher popularity breed dogs
            // are generated than lower popularity breed dogs
            popularities = Enumerable.Range(1, 200).ToList();
            popularityCounts[0] = 0;
            foreach (var popularity in popularities)
                popularityCounts[popularity] = Convert.ToInt32(popularityCounts[popularity-1] + 1000/(1 + 0.4 * popularity));

            var allBreeds = _dbContext.DogBreeds.OrderBy(db => db.BreedPopularity);
            foreach (var breed in allBreeds)
            {
                if (!availableBreeds.ContainsKey(breed.BreedPopularity))
                    availableBreeds[breed.BreedPopularity] = new List<DogBreed>();
                availableBreeds[breed.BreedPopularity].Add(breed);
            }
        }

        private Tuple<string,string> GenerateName()
        {
            var random = new Random();
            return new Tuple<string, string>(_firstNames[random.Next(_firstNames.Count)], _lastNames[random.Next(_lastNames.Count)]);
        }

        private string GeneratePhoneNumber()
        {
            var random = new Random();
            string numericChars = "0123456789";
            return new string(Enumerable.Repeat(numericChars,10).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateEmail(string firstname, string lastname)
        {
            var random = new Random();
            return String.Concat(firstname, lastname, "@", _emailDomains[random.Next(_emailDomains.Count)]);
        }

        private DogOwner GenerateOwner()
        {
            var name = GenerateName();
            var random = new Random();
            return new DogOwner()
            {
                Id = Guid.NewGuid(),
                FirstName = name.Item1,
                LastName = name.Item2,
                Age = random.Next(20, 90),
                CountryCode = _countryCodes[random.Next(_countryCodes.Count)],
                Phone = GeneratePhoneNumber(),
                Email = GenerateEmail(name.Item1, name.Item2)
            };
        }

        public List<DogOwner> GenerateOwners(int number=1,bool save=false)
        {
            var owners = new List<DogOwner>();
            while (owners.Count < number)
                owners.Add(GenerateOwner());
            if (save)
            {
                _dbContext.AddRange(owners);
                _dbContext.SaveChanges();
            }
            return owners;
        }

        private DogBreed GetDogBreed()
        {
            var random = new Random();

            DogBreed breed = null;
            while (breed == null)
            {
                var number = random.Next(popularityCounts[0],popularityCounts[200]);

                foreach (var popularity in popularities)
                {
                    if (number >= popularityCounts[popularity-1] && number <= popularityCounts[popularity])
                    {
                        if (availableBreeds.TryGetValue(popularity, out var breeds))
                        {
                            breed = breeds.Count > 1 ? breeds[random.Next(0, breeds.Count)] : breeds[0];
                            break;
                        }
                    }
                }
            }

            return breed;
        }


        private DogOwner GetOwner(List<DogOwner> owners)
        {
            var random = new Random();
            var skip = (int)(random.NextDouble() * _dbContext.DogOwners.Count());
            // First, add to "new" owners that don't have any dogs.
            if (owners.Any(o => o.Dogs.Count == 0))
            {
                skip = (int)(random.NextDouble() * owners.Where(o => o.Dogs.Count == 0).Count());
                return owners.Where(o => o.Dogs.Count == 0).OrderBy(o => o.Dogs.Count).Skip(skip).Take(1).First();
            }
            // Then, add to "newish" owners that have fewer than half of the dogs of the highest dog-count owner.
            if (owners.Any(o => o.Dogs.Count <= owners.Max(o => o.Dogs.Count)/2))
            {
                skip = (int)(random.NextDouble() * owners.Where(o => o.Dogs.Count <= owners.Max(o => o.Dogs.Count)/2).Count());
                return owners.Where(o => o.Dogs.Count <= owners.Max(o => o.Dogs.Count) / 2).OrderBy(o => o.Dogs.Count).Skip(skip).Take(1).First();
            }
            // Then, add from the owners with the fewest dogs to the most. 
            skip = (int)(random.NextDouble() * owners.Count());
            return owners.OrderBy(o => o.Dogs.Count).Skip(skip).Take(1).First();
        }

        private Dog GenerateDog(List<DogOwner> owners)
        {
            var random = new Random();
            var breed = GetDogBreed();
            var owner = GetOwner(owners);
            var dog = new Dog()
            {
                Id = Guid.NewGuid(),
                BreedId = breed.Id,
                Breed = breed,
                Name = _dogNames[random.Next(_dogNames.Count)],
                Age = random.Next(0, breed.MaximumLifeExpectancy + 1),
                OwnerId = owner.Id,
                Owner = owner
            };
            owner.Dogs.Add(dog);
            return dog;
        }

        public List<Dog> GenerateDogs(int number=1,bool save=false)
        {
            var owners = _dbContext.DogOwners.ToList();
            var dogs = new List<Dog>();
            while (dogs.Count < number)
                dogs.Add(GenerateDog(owners));
            if (save)
            {
                _dbContext.AddRange(dogs);
                _dbContext.SaveChanges();
            }
            return dogs;
        }

    }
}
