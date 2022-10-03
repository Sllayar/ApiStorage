using ApiStorage.Models;
using ApiStorage.Models.Bus;
using ApiStorage.Models.Mongo;

using CommonStorage.Mongo;

using MassTransit;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiStorage.MassTransit
{
    public class TaskQueueAddTestData : IConsumer<BusDataTest>
    {
        private readonly ILogger<TaskQueueAddTestData> _logger;
        private readonly IBus _bus;
        private readonly TestingTaskService _testingTaskService;

        public TaskQueueAddTestData(
            TestingTaskService testingTaskService, IBus bus, ILogger<TaskQueueAddTestData> logger)
        {
            _testingTaskService = testingTaskService;
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<BusDataTest> context)
        {
            _logger.LogInformation("Начало работы TaskQueueAddTestData. Источник: " + context.Message);

            var data = JsonConvert.SerializeObject(
                new AttenuatioPhaseShiftEntity()
                {
                    CreateDate = DateTime.Now,

                    UpdateDate = DateTime.Now,

                    TestGroupId = context.Message.TestingTaskEntityId,

                    TestName = nameof(AttenuatioPhaseShiftEntity),

                    Comment = "Save from TaskQueueAddTestData.Consume",

                    Result = new ResultParameter()
                    {
                        Parameters = new List<AttenuatioPhaseShiftChannelData>()
                        {
                            new AttenuatioPhaseShiftChannelData()
                            {
                                FullResult = true,

                                Parameters = new List<AttenuationPhaseShiftData>()
                                {
                                    new AttenuationPhaseShiftData()
                                    {
                                        PhaseData = new List<Tuple<double, double>>()
                                        {
                                            new Tuple<double, double>(100.001f, 100.001f),
                                            new Tuple<double, double>(100.002f, 100.0012f),
                                            new Tuple<double, double>(100.003f, 100.0013f)
                                        },
                                        AmpData =new List<Tuple<double, double>>()
                                        {
                                            new Tuple<double, double>(200.001f, 200.001f),
                                            new Tuple<double, double>(200.002f, 200.0012f),
                                            new Tuple<double, double>(200.003f, 200.0013f)
                                        }
                                    }
                                }
                            }
                        }
                    }
                });

            var busData = new BusNonTypedData()
            {
                Key = "AttenuatioPhaseShift",

                TaskId = context.Message.TestingTaskEntityId,

                Data = data
            };

            await _bus.Publish(busData);

            _logger.LogInformation("Завершение работы TaskQueueAddTestData");
        }
    }
}
