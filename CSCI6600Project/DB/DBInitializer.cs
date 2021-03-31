using CSCI6600Project.DataGeneration;
using CSCI6600Project.Models;
using CSCI6600Project.Models.Context;
using CSCI6600Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.DB
{
    public static class DBInitializer
    {
        private const int ownerCount = 50000;
        private const int dogCount = 150000;

        public static void Initialize(csci6600Context dbContext)
        {
            var generator = new GeneratorService(dbContext);

            var currentOwnerCount = dbContext.DogOwners.Count();
            var batchSize = 1000;
            var numberNeeded = ownerCount - currentOwnerCount;
            while(currentOwnerCount < ownerCount)
            {
                generator.GenerateOwners(numberNeeded < batchSize ? numberNeeded : batchSize, true);
                currentOwnerCount += (numberNeeded < batchSize ? numberNeeded : batchSize);
                numberNeeded = ownerCount - currentOwnerCount;
            }

            var currentDogCount = dbContext.Dogs.Count();
            batchSize = 1000;
            numberNeeded = dogCount - currentDogCount;
            while(currentDogCount < dogCount)
            {
                generator.GenerateDogs(numberNeeded < batchSize ? numberNeeded : batchSize, true);
                currentDogCount += (numberNeeded < batchSize ? numberNeeded : batchSize);
                numberNeeded = dogCount - currentDogCount;
            }
        }
    }
}
