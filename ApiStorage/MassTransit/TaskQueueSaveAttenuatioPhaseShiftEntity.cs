using ApiStorage.Models;
using ApiStorage.Models.Mongo;
using ApiStorage.Mongo;

using CommonStorage.Mongo;

using MassTransit;

using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace ApiStorage.MassTransit
{
    public class TaskQueueSaveAttenuatioPhaseShiftEntity : IConsumer<AttenuatioPhaseShiftEntity>
    {
        private readonly ILogger<TaskQueueSaveAttenuatioPhaseShiftEntity> _logger;
        private readonly TestingTaskService _testingTaskService;
        private readonly AttenuatioPhaseShiftServices _attenuatioPhaseShiftServices;
        private readonly MongoSaveService _mongoSaveService;

        public TaskQueueSaveAttenuatioPhaseShiftEntity(
             MongoSaveService mongoSaveService,
            AttenuatioPhaseShiftServices attenuatioPhaseShiftServices,
            TestingTaskService testingTaskService, ILogger<TaskQueueSaveAttenuatioPhaseShiftEntity> logger)
        {
            _logger = logger;
            _testingTaskService = testingTaskService;
            _attenuatioPhaseShiftServices = attenuatioPhaseShiftServices;
            _mongoSaveService = mongoSaveService;
        }

        public async Task Consume(ConsumeContext<AttenuatioPhaseShiftEntity> context)
        {
            _logger.LogInformation("Сохранение TaskQueueSaveAttenuatioPhaseShiftEntity " + context.Message.TestGroupId);

            var taskEntity = await _testingTaskService.GetAsync(context.Message.TestGroupId);

            try
            {
                await _attenuatioPhaseShiftServices.CreateAsync(context.Message);

                taskEntity.Status = CommonStorage.Models.Mongo.Status.Successful;
                taskEntity.Reference = context.Message.Id;

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка TaskQueueSaveAttenuatioPhaseShiftEntity " + context.Message.TestGroupId);

                _logger.LogError(ex.Message);

                taskEntity.Status = CommonStorage.Models.Mongo.Status.Mistake;

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);

                throw ex;
            }

            _logger.LogInformation("Финиш TaskQueueSaveAttenuatioPhaseShiftEntity " + context.Message.TestGroupId);
        }
    }
}
