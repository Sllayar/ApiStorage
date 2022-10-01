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

                taskEntity.SaveData.Add(
                    new CommonStorage.Models.Mongo.TestingTaskEntity.LoadData()
                    {
                        Name = nameof(AttenuatioPhaseShiftEntity),
                        Reference = context.Message.Id,
                        Status = CommonStorage.Models.Mongo.TestingTaskEntity.Status.Successful
                    });

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка TaskQueueSaveAttenuatioPhaseShiftEntity " + context.Message.TestGroupId);

                _logger.LogError(ex.Message);

                taskEntity.SaveData.Add(
                    new CommonStorage.Models.Mongo.TestingTaskEntity.LoadData()
                    {
                        Name = nameof(AttenuatioPhaseShiftEntity),
                        Status = CommonStorage.Models.Mongo.TestingTaskEntity.Status.Mistake
                    });

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);

                throw ex;
            }

            _logger.LogInformation("Финиш TaskQueueSaveAttenuatioPhaseShiftEntity " + context.Message.TestGroupId);
        }
    }
}
