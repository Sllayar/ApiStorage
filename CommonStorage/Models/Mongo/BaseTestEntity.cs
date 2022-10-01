using ApiStorage.Models.Mongo;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonStorage.Models.Mongo
{
    public class BaseTestEntity : ITestMongoModel
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)] 
        public Guid Id { get; set; }

        public Guid TestGroupId { get; set; }

        public string Comment { get; set; }

        public string TestName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
