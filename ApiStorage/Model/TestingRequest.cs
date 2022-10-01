using ApiStorage.Models;
using ApiStorage.Models.Mongo;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace ApiStorage.Models
{
    public class TestingRequest
    {
        public string TestName { get; set; }

        public string Comment { get; set; }

        public AttenuatioPhaseShiftEntity TestResult {get; set;}
    }
}
