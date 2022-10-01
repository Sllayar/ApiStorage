using Microsoft.Extensions.Configuration;

using System.Linq;

namespace ApiStorage.Tools.Context
{
    public class MongoContext
    {
        protected readonly string _connection;
        protected readonly string _dbName;

        public MongoContext(IConfiguration configuration)
        {
            _connection = configuration
                .GetSection("DbConnection")
                .GetSection("MongoDB")
                .GetChildren()
                .FirstOrDefault(value => value.Key == "Connection")?.Value;
                
            _dbName = configuration
                .GetSection("DbConnection")
                .GetSection("MongoDB")
                .GetChildren()
                .FirstOrDefault(value => value.Key == "DbName")?.Value;
        }
    }
}
