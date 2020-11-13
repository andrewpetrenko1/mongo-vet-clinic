using MongoDB.Bson;

namespace mongo_vet_clinic.Entities
{
  public interface IEntity
  {
    public ObjectId Id { get; set; }
  }
}
