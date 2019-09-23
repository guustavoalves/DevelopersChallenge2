using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Desafio.Nibo.Infrastructure.Context
{
    public class MongoDbContext<T> where T : class
    {
        public IMongoCollection<T> collection { get; }
        private IMongoDatabase database;
        private readonly string collectionName;

        public MongoDbContext(string connectionString, string database, string collectionName)
        {            
            this.database = new MongoClient(connectionString).GetDatabase(database);            
            collection = this.database.GetCollection<T>(collectionName);
            this.collectionName = collectionName;
        }

        public List<T> Find()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public T FindById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
            return collection.Find(filter).First();
        }

        public void Insert(List<T> record)
        {
            foreach (var item in record)
            {
                collection.InsertOne(item);
            }
        }

        public void Update(T record, string id)
        {
            collection.ReplaceOne(new BsonDocument("_id", new ObjectId(id)), record, new UpdateOptions { IsUpsert = true });
        }

        public void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
            collection.DeleteOne(filter);
        }

        public void DeleteAll()
        {
            database.DropCollection(collectionName);
        }
    }
}
