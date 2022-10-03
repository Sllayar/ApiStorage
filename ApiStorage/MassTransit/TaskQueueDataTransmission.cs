using ApiStorage.Models;
using ApiStorage.Models.Bus;
using ApiStorage.Models.Mongo;
using ApiStorage.Mongo;

using CommonStorage.Mongo;

using MassTransit;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStorage.MassTransit
{
    public class TaskQueueDataTransmission : IConsumer<BusNonTypedData>
    {
        private readonly TestingTaskService _testingTaskService;
        private readonly ILogger<TaskQueueDataTransmission> _logger;
        private readonly IBus _bus;

        public TaskQueueDataTransmission(
            IBus bus, ILogger<TaskQueueDataTransmission> logger, TestingTaskService testingTaskService)
        {
            _testingTaskService = testingTaskService;
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<BusNonTypedData> context)
        {
            _logger.LogInformation("начало TaskQueueDataTransmission");

            var taskEntity = await _testingTaskService.GetAsync(context.Message.TaskId);
            taskEntity.Status = CommonStorage.Models.Mongo.Status.Processed;
            taskEntity.Type = context.Message.Key;

            if (context.Message.Key == "AttenuatioPhaseShift")
            {
                await _bus.Publish(
                    JsonConvert.DeserializeObject<AttenuatioPhaseShiftEntity>(context.Message.Data));
            }
            else
            {
                _logger.LogError("TaskQueueDataTransmission not faund Type " + context.Message.Key);

                taskEntity.Status = CommonStorage.Models.Mongo.Status.Mistake;
            }

            await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);

            _logger.LogInformation("Финиш DataTransmission");
        }
    }
}
