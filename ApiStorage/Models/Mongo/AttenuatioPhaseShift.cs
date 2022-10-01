using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStorage.Models.Mongo
{
    public class AttenuatioPhaseShift
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid TaskId {get; set;}

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public ResultParameter Result { get; set; }
    }
}
