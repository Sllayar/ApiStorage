using ApiStorage.Models.Mongo;

using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonStorage.Models.Mongo
{

    public class TestingTaskEntity : IMongoEntity
    {
        public enum Status
        {
            Processed,
            Successful,
            Mistake
        }

        public class LoadData
        { 
            public string Name { get; set; }

            public Guid Reference { get; set; }

            public Status Status { get; set; }
        }


        [BsonId]
        public Guid Id { get; set; }
        public string TestName { get; set; }
        public string TestComment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public List<LoadData> SaveData { get; set; }
    }
}
