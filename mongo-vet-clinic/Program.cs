using System;
using mongo_vet_clinic.Entities;
using mongo_vet_clinic.Repositories;
using MongoDB.Driver;

namespace mongo_vet_clinic
{
  class Program
  {
    static void Main(string[] args)
    {
      var client = new MongoClient("mongodb://localhost:27017");
      var database = client.GetDatabase("vet_clinic");
      var repo = new PetRepository(database);
      repo.InitCollection();
      repo.GetSortedPetsPages(3);
      repo.GererateReport();
    }
  }
}
