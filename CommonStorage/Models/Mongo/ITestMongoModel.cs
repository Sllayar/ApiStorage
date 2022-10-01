using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace ApiStorage.Models.Mongo
{
    public interface ITestMongoModel : IMongoEntity
    {
        public Guid TestGroupId { get; set; }

        public string Comment { get; set; }
    }
}
