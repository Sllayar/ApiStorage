using ApiStorage.Models.Mongo;

using MongoDB.Bson.Serialization.Attributes;

using System;

namespace CommonStorage.Models.Mongo
{
    public enum Status
    {
        GetOn,
        Processed,
        Successful,
        Mistake
    }

    public class TestingTaskEntity : IMongoEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }

        public Guid Reference { get; set; }

        public string TestName { get; set; }

        public string Comment { get; set; }

        public string Type { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Status Status { get; set; }
    }
}
