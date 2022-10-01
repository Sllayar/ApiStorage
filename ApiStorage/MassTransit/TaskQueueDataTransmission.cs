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
        //private class Deserializer <T>
        //{
        //    public T Deserialize(string obj) => JsonConvert.DeserializeObject <T> (obj);
        //};

        //public Dictionary<string, Type> KeyTypesPairs = new Dictionary<string, Type>()
        //{
        //    { "AttenuatioPhaseShift", typeof(AttenuatioPhaseShiftEntity) }
        //};

        private readonly ILogger<TaskQueueDataTransmission> _logger;
        private readonly IBus _bus;
        private readonly TestingTaskService _testingTaskService;

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

            if (context.Message.Key == "AttenuatioPhaseShift")
            {
                await _bus.Publish(
                    JsonConvert.DeserializeObject<AttenuatioPhaseShiftEntity>(context.Message.Data));
            }
            else
            {
                // TODO
                _logger.LogError("TaskQueueDataTransmission not faund Type " + context.Message.Key);

                var taskEntity = await _testingTaskService.GetAsync(context.Message.TaskId);

                taskEntity.SaveData.Add(
                    new CommonStorage.Models.Mongo.TestingTaskEntity.LoadData()
                    {
                        Name = context.Message.Key,
                        Reference = context.Message.TaskId,
                        Status = CommonStorage.Models.Mongo.TestingTaskEntity.Status.Mistake
                    });

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);
            }

            _logger.LogInformation("Финиш DataTransmission");

            //TODO
            //Type dataType;
            //if (KeyTypesPairs.TryGetValue(context.Message.Key, out dataType))
            //{
            //var type = typeof(Deserializer<>).MakeGenericType(dataType.GetType());
            //dynamic deserializer = Activator.CreateInstance(type);
            //var res = deserializer.Deserialize(context.Message.Data);
            //_bus.Publish(res);
            //}
        }
    }
}
