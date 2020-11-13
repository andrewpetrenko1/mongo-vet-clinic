using mongo_vet_clinic.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace mongo_vet_clinic.Repositories
{
  public class Repository<TEntity> where TEntity : IEntity
  {
    protected readonly IMongoCollection<TEntity> collection;
    public Repository(IMongoDatabase database)
    {
      collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public ObjectId Insert(TEntity entity)
    {
      collection.InsertOne(entity);
      return entity.Id;
    }

    public IEnumerable<TEntity> InsertList(IEnumerable<TEntity> entities)
    {
      collection.InsertMany(entities);
      return entities;
    }

    public IList<TEntity> GetAll()
    {
      return collection.Find(_ => true).ToList();
    }

    public TEntity Get(ObjectId id)
    {
      var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
      return collection.Find(filter).FirstOrDefault();
    }

    public bool EmptyCollection()
    {
      return collection.EstimatedDocumentCount() == 0;
    }
  }
}
