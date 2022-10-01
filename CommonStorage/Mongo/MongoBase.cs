using ApiStorage.Models.Mongo;
using ApiStorage.Tools.Context;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonStorage.Mongo
{
    public abstract class MongoBase<EntityType> : MongoContext where EntityType : class, IMongoEntity
    {
        protected readonly IMongoCollection<EntityType> _collection;

        public MongoBase(IConfiguration configuration, string collectionName) : base(configuration)
        {
            var mongoClient = new MongoClient(_connection);

            var mongoDatabase = mongoClient.GetDatabase(_dbName);

            _collection = mongoDatabase.GetCollection<EntityType>(collectionName);
        }

        public IMongoQueryable<EntityType> GetQuery() => _collection.AsQueryable();

        public async Task<List<EntityType>> GetAsync() =>
           await _collection.Find(_ => true).ToListAsync();

        public async Task<EntityType> GetAsync(Guid id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(EntityType data) =>
            await _collection.InsertOneAsync(data);

        public async Task UpdateAsync(Guid id, EntityType updatedata) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedata);

        public async Task RemoveAsync(Guid id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
