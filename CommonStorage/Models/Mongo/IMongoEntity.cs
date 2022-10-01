using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace ApiStorage.Models.Mongo
{
    public interface IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        public string TestName { get; set; }

        DateTime CreateDate { get; set; }

        DateTime UpdateDate { get; set; }
    }
}
