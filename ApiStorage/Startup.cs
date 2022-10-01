using ApiStorage.MassTransit;
using ApiStorage.Mongo;

using CommonStorage.Tools;

using GreenPipes;

using MassTransit;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStorage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Добавляем масстрансит

            services.AddMassTransit(x =>
                {
                    x.AddConsumer<TaskQueueAddTestData>();
                    x.AddConsumer<TaskQueueDataTransmission>();
                    x.AddConsumer<TaskQueueSaveAttenuatioPhaseShiftEntity>();
                    //x.AddConsumer<TaskQueueMongoSave>();

                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                        cfg.ConcurrentMessageLimit = 5;
                        cfg.PrefetchCount = 5;
                        cfg.UseTimeout(timeOut => timeOut.Timeout = new TimeSpan(1, 0, 0));
                        cfg.UseMessageRetry(r => r.Immediate(3));
                    });
                });

            services.AddMassTransitHostedService(true);
            #endregion

            StartupConfig.AddMongoServices(services);
            StartupConfig.AddHealthChecksService(services);

            services.AddSingleton<MongoSaveService>();

            services.AddMvc(options => {options.InputFormatters.Insert(0, new RawJsonBodyInputFormatter());});

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StartupConfig.ConfigureHealthChecks(app, env.IsDevelopment());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
