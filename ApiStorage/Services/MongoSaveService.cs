using ApiStorage.Models;
using ApiStorage.Models.Mongo;
using ApiStorage.Mongo;

using CommonStorage.Models.Mongo;
using CommonStorage.Mongo;

using MassTransit;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStorage.MassTransit
{
    public class MongoSaveService
    {
        private readonly ILogger<TaskQueueSaveAttenuatioPhaseShiftEntity> _logger;
        private readonly TestingTaskService _testingTaskService;

        public MongoSaveService(TestingTaskService testingTaskService, 
            ILogger<TaskQueueSaveAttenuatioPhaseShiftEntity> logger)
        {
            _logger = logger;
            _testingTaskService = testingTaskService;
        }

        public async Task BaseConsume(ConsumeContext<BaseTestEntity> context, 
            MongoBase<IMongoEntity> mongoBase, IMongoEntity testData)
        {
            _logger.LogInformation("Сохранение " + context.Message.TestName + " " + context.Message.TestGroupId);

            var taskEntity = await _testingTaskService.GetAsync(context.Message.TestGroupId);

            try
            {
                await mongoBase.CreateAsync(testData);

                taskEntity.SaveData.Add(
                    new TestingTaskEntity.LoadData()
                    {
                        Name = nameof(AttenuatioPhaseShiftEntity),
                        Reference = context.Message.Id,
                        Status = TestingTaskEntity.Status.Successful
                    });

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка " + context.Message.TestName + " " + context.Message.TestGroupId);

                _logger.LogError(ex.Message);

                taskEntity.SaveData.Add(
                    new TestingTaskEntity.LoadData()
                    {
                        Name = nameof(AttenuatioPhaseShiftEntity),
                        Status = TestingTaskEntity.Status.Mistake
                    });

                await _testingTaskService.UpdateAsync(taskEntity.Id, taskEntity);

                throw ex;
            }

            _logger.LogInformation("Финиш " + context.Message.TestName + " " + context.Message.TestGroupId);
        }
    }
}
