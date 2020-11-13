using mongo_vet_clinic.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mongo_vet_clinic.Repositories
{
  public class PetRepository : Repository<Pet>
  {
    public PetRepository(IMongoDatabase database) : base(database)
    {
      collection.Indexes.CreateOne(new CreateIndexModel<Pet>("{ RegDate: -1 }"));
    }

    public void GetSortedPetsPages(int pageSize)
    {
      var pageCount = (int)Math.Ceiling((double)collection.EstimatedDocumentCount() / pageSize );

      for (int i = 0; i < pageCount; i++)
      {
        Console.WriteLine($"---- Page: {(i + 1 == pageCount ? "Last page" : $"{i + 1}")} | Total pages: {pageCount} ----");
        var petsList = GetPetsPage(i, pageSize);

        foreach (var pet in petsList)
          PrintPet(pet);

        if(i + 1 == pageCount)
          break;

        Console.WriteLine("Press any key to view next page\n");
        Console.ReadKey();
        Console.Clear();
      }
    }

    public void GererateReport()
    {
      var report = collection.Aggregate()
        .Group(p => p.PetType, g => new { Type = g.Key, Count = g.Count() })
        .SortByDescending(g => g.Count)
        .ToList();

      Console.WriteLine("\n---- Report ----");
      foreach (var elem in report)
        Console.WriteLine($"Pet type: {elem.Type, 5} | Count: {elem.Count}");
    } 

    private IEnumerable<Pet> GetPetsPage(int pageNum, int pageSize)
    {
      return collection.Find(_ => true)
        .SortByDescending(p => p.RegDate)
        .Skip(pageNum * pageSize)
        .Limit(pageSize)
        .ToList();
    }

    private void PrintPet(Pet pet)
    {
      Console.WriteLine($"\nPet name: {pet.Name}");
      Console.WriteLine($"Pet type: {pet.PetType}");
      Console.WriteLine($"Registration date: {pet.RegDate}");
      Console.WriteLine($"\tOwner name: {pet.Owner.Name}");
      Console.WriteLine($"\tOwner phone: {pet.Owner.Pnone}");
    }

    public void InitCollection()
    {
      if (!EmptyCollection())
      {
        Console.WriteLine("Collection not empty");
        return;
      }
      List<Pet> pets = new List<Pet>()
      {
        new Pet
        {
          Name = "Archi", PetType = PetType.Dog, RegDate = DateTime.Now,
          Owner = new Owner
          {
            Name = "Artem", Pnone = "38097555554"
          }
        },
        new Pet
        {
          Name = "Ostin", PetType = PetType.Cat, RegDate = DateTime.Now.AddMinutes(-6),
          Owner = new Owner
          {
            Name = "Vlad", Pnone = "38097444444"
          }
        },
        new Pet
        {
          Name = "Kesha", PetType = PetType.Bird, RegDate = DateTime.Now.AddMinutes(-4),
          Owner = new Owner
          {
            Name = "Grisha", Pnone = "380971123334"
          }
        },
        new Pet
        {
          Name = "Max", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-1),
          Owner = new Owner
          {
            Name = "Anton", Pnone = "380564123334"
          }
        },
        new Pet
        {
          Name = "Baron", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-10),
          Owner = new Owner
          {
            Name = "Valera", Pnone = "38056442334"
          }
        },
        new Pet
        {
          Name = "Bella", PetType = PetType.Cat, RegDate = DateTime.Now.AddMinutes(-1),
          Owner = new Owner
          {
            Name = "Masha", Pnone = "3805643323234"
          }
        },
        new Pet
        {
          Name = "Kitty", PetType = PetType.Cat, RegDate = DateTime.Now.AddMinutes(-5),
          Owner = new Owner
          {
            Name = "Nastya", Pnone = "380334555314"
          }
        },
        new Pet
        {
          Name = "Buddy", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-3),
          Owner = new Owner
          {
            Name = "Nikita", Pnone = "380124123334"
          }
        },
        new Pet
        {
          Name = "Daisy", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-9),
          Owner = new Owner
          {
            Name = "Natasha", Pnone = "3804553334"
          }
        },
        new Pet
        {
          Name = "Molly", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-7),
          Owner = new Owner
          {
            Name = "Ruslan", Pnone = "3804874123334"
          }
        },
        new Pet
        {
          Name = "Tiki", PetType = PetType.Bird, RegDate = DateTime.Now.AddMinutes(-8),
          Owner = new Owner
          {
            Name = "Taras", Pnone = "380565221114"
          }
        },
        new Pet
        {
          Name = "Kiwi", PetType = PetType.Bird, RegDate = DateTime.Now.AddMinutes(-6),
          Owner = new Owner
          {
            Name = "Vlad", Pnone = "38056453213"
          }
        },
        new Pet
        {
          Name = "Sunny", PetType = PetType.Bird, RegDate = DateTime.Now.AddMinutes(-4),
          Owner = new Owner
          {
            Name = "Kirill", Pnone = "3806556334"
          }
        },
        new Pet
        {
          Name = "Tweety", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-4),
          Owner = new Owner
          {
            Name = "Katya", Pnone = "38056123145"
          }
        },
        new Pet
        {
          Name = "Oliver", PetType = PetType.Dog, RegDate = DateTime.Now.AddMinutes(-3),
          Owner = new Owner
          {
            Name = "Nastya", Pnone = "3805653214"
          }
        }
      };

      InsertList(pets);
      Console.WriteLine("Collection initialized");
    }
  }
}
