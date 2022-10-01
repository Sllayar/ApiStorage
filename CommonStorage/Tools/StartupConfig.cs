using ApiStorage.Mongo;

using CommonStorage.Mongo;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonStorage.Tools
{
    public static class StartupConfig
    {

        #region Добавляем масстрансит

        public static void AddMassTransitService(IServiceCollection services)
        {
            throw new NotImplementedException("Not implemented");
        }

        #endregion

        #region Добавляем монго провайдеры
        public static void AddMongoServices(IServiceCollection services)
        {
            services.AddSingleton<TestingTaskService>();

            services.AddSingleton<AttenuatioPhaseShiftServices>();
        }

        #endregion

        #region Добавляем HealthChecks
        public static void AddHealthChecksService(IServiceCollection services)
        {
            services.AddHealthChecks()
              .AddCheck<MongoHealthCheck>(
                "MongoDBConnectionCheck",
                tags: new[] { "mongo" });

            services.AddHealthChecks()
              .AddCheck<BaseHealthCheck>(
                "ConnectionCheck",
                tags: new[] { "Base" });
        }

        public static void ConfigureHealthChecks(IApplicationBuilder app, bool devMod)
        {
            app.UseHealthChecks("/health/api", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("Base")
            });
            app.UseHealthChecks("/health/mongo");
        }
        #endregion
    }
}
