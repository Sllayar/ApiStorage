using ApiStorage.Tools.Context;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ApiStorage.Mongo
{
    public class MongoHealthCheck : MongoContext, IHealthCheck
    {
        private IMongoDatabase _db { get; set; }
        public MongoClient _mongoClient { get; set; }

        public MongoHealthCheck(IConfiguration configuration) : base(configuration)
        {
            _mongoClient = new MongoClient(_connection);

            _db = _mongoClient.GetDatabase(_dbName);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var healthCheckResultHealthy = await CheckMongoDBConnectionAsync();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            if (!healthCheckResultHealthy) return HealthCheckResult.Unhealthy("MongoDB health check failure");
            if (ts.TotalSeconds > 3) return HealthCheckResult.Degraded("MongoDB health check degraded response");
             
            return HealthCheckResult.Healthy("MongoDB health check success");
        }

        private async Task<bool> CheckMongoDBConnectionAsync()
        {
            try { await _db.RunCommandAsync((Command<BsonDocument>)"{ping:1}"); }
            catch (Exception) { return false; }

            return true;
        }
    }
}
