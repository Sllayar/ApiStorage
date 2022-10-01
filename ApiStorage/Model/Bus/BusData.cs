using CommonStorage.Models.Mongo;

using System;
using System.Collections.Generic;
using System.Text;

namespace ApiStorage.Models.Bus
{
    public class BusData
    {
        public DateTime DateTime { get; set; }

        public Guid TestingTaskEntityId { get; set; }

        public List<TestingRequest> TestingRequest { get; set; }
    }
}
