CREATE NONCLUSTERED INDEX IDX_Dogs_BreedIdNameOwnerId
	ON Dogs (BreedId,Name,OwnerId);
CREATE NONCLUSTERED INDEX IDX_Dogs_NameOwnerId
	ON Dogs (Name,OwnerId);
CREATE NONCLUSTERED INDEX IDX_Dogs_OwnerId
	ON Dogs (OwnerId);
CREATE NONCLUSTERED INDEX IDX_DogOwners_FirstNameLastNameCountry
	ON DogOwners (FirstName,LastName,CountryCode);
CREATE NONCLUSTERED INDEX IDX_DogOwners_LastNameCountry
	ON DogOwners (LastName,CountryCode);
CREATE NONCLUSTERED INDEX IDX_DogOwners_Country
	ON DogOwners (CountryCode);
CREATE NONCLUSTERED INDEX IDX_DogBreeds_NamePopularityGroupId
	ON DogBreeds (Name,BreedPopularity,GroupId);
CREATE NONCLUSTERED INDEX IDX_DogBreeds_PopularityGroupId
	ON DogBreeds (BreedPopularity,GroupId);
CREATE NONCLUSTERED INDEX IDX_DogBreeds_GroupId
	ON DogBreeds (GroupId);
CREATE NONCLUSTERED INDEX IDX_BreedGroups_Name
	ON BreedGroups (Name);

