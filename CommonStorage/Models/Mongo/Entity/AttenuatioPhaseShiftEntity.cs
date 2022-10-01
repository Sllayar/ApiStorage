using CommonStorage.Models.Mongo;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;

namespace ApiStorage.Models.Mongo
{
    public class AttenuatioPhaseShiftEntity : BaseTestEntity, ITestData
    {
        public ResultParameter Result { get; set; }
    }
}
