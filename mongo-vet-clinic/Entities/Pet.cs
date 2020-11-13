using MongoDB.Bson;
using System;

namespace mongo_vet_clinic.Entities
{
  public class Pet : IEntity
  {
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public PetType PetType { get; set; }
    public DateTime RegDate { get; set; }
    public Owner Owner { get; set; }
  }

  public enum PetType
  {
    Cat,
    Dog,
    Bird
  }
}
