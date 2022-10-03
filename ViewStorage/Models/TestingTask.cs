using CommonStorage.Models.Mongo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewStorage.Models
{
    public class TestingTask
    {
        public Guid Id { get; set; }

        public string TestName { get; set; }

        public string TestComment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Name { get; set; }

        public Guid Reference { get; set; }

        public Status Status { get; set; }
        
    }
}
