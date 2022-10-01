using ApiStorage.Models;
using ApiStorage.Models.Mongo;
using ApiStorage.Mongo;

using CommonStorage.Models;
using CommonStorage.Models.Mongo;
using CommonStorage.Mongo;

using MassTransit;

using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace ApiStorage.MassTransit
{
    //public class TaskQueueMongoSave : IConsumer<BusData>
    //{
    //    private readonly AttenuatioPhaseShiftServices _attenuatioPhaseShiftServices;
    //    private readonly TestingTaskService _testingTaskService;
    //    private readonly ILogger<TaskQueueMongoSave> _logger;

    //    public TaskQueueMongoSave( 
    //        TestingTaskService testingTaskService,
    //        AttenuatioPhaseShiftServices attenuatioPhaseShiftServices,
    //        ILogger<TaskQueueMongoSave> logger)
    //    {
    //        _testingTaskService = testingTaskService;
    //        _attenuatioPhaseShiftServices = attenuatioPhaseShiftServices;
    //        _logger = logger;
    //    }

    //    public async Task Consume(ConsumeContext<BusData> context)
    //    {
    //        _logger.LogInformation("Начало работы TaskQueueMongoSave");

    //        AttenuatioPhaseShiftEntity p = context.Message.TestingRequest[0].TestResult as AttenuatioPhaseShiftEntity;

    //        var task = await _testingTaskService.GetAsync(context.Message.TestingTaskEntityId);

    //        foreach (var newData in context.Message.TestingRequest)
    //        {
    //            try
    //            {
    //                var id = await SaveToMongo(newData, context.Message);
    //            }
    //            catch (Exception ex)
    //            {
    //                task.SaveStatus = TestingTaskEntity.Status.Mistake;
    //                await _testingTaskService.UpdateAsync(context.Message.TestingTaskEntityId, task);

    //                //TODO Сохранять ошибки и данные которые не сохранились.
    //            }
    //        }

    //        if (task.SaveStatus != TestingTaskEntity.Status.Mistake)
    //        {
    //            task.SaveStatus = TestingTaskEntity.Status.Successful;

    //            await _testingTaskService.UpdateAsync(context.Message.TestingTaskEntityId, task);
    //        }

    //        _logger.LogInformation("Завершение работы TaskQueueMongoSave TaskId = " +
    //            context.Message.TestingTaskEntityId);
    //    }

    //    private async Task<Guid?> SaveToMongo(TestingRequest newData, BusData message)
    //    {
    //        AttenuatioPhaseShiftEntity p = newData.TestResult as AttenuatioPhaseShiftEntity;
    //        AttenuatioPhaseShiftEntity p1 = (AttenuatioPhaseShiftEntity)newData.TestResult;

    //        // TODO сделать обобщенную реализацию. 
    //        if (newData.TestResult is AttenuatioPhaseShiftEntity)
    //        {
    //            // TODO сделать прокидывания дальше в шину. Тут реализовывать только парсинг формата. 
    //            // TODO сделать парсер формата. Иначе могут быть ошибки на фронте.
    //            var attenuatioPhaseShift = new AttenuatioPhaseShiftEntity()
    //            {
    //                TestGroupId = message.TestingTaskEntityId,
    //                Result = newData.TestResult.Result,
    //                TestName = nameof(ResultParameter),
    //                UpdateDate = message.DateTime,
    //                CreateDate = message.DateTime
    //            };

    //            await _attenuatioPhaseShiftServices.CreateAsync(attenuatioPhaseShift);

    //            _logger.LogError("Успешное сохранение задания " + attenuatioPhaseShift.Id + 
    //                " Группа " + message.TestingTaskEntityId);

    //            return attenuatioPhaseShift.Id;
    //        }
    //        else
    //        {
    //            _logger.LogError("Ошибка сохранения TaskQueueMongoSave Тип не найден TaskId = " +
    //                message.TestingTaskEntityId);

    //            throw new Exception("Неизвестный тип");
    //        }
    //    }
    //}
}
