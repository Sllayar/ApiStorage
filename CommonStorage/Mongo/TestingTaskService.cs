using CommonStorage.Models.Mongo;

using Microsoft.Extensions.Configuration;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonStorage.Mongo
{
    public class TestingTaskService : MongoBase<TestingTaskEntity>
    {
        public TestingTaskService(IConfiguration configuration) : 
            base(configuration, nameof(TestingTaskEntity))
        {   }
    }
}
