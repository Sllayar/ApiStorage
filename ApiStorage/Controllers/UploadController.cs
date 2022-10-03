using ApiStorage.Models;
using ApiStorage.Models.Bus;
using ApiStorage.Models.Mongo;
using ApiStorage.Mongo;

using CommonStorage.Models;
using CommonStorage.Models.Mongo;
using CommonStorage.Mongo;

using MassTransit;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiStorage.Controllers
{
    [ApiController]
    [Route("api")]
    public class UploadController : Controller
    {
        private readonly TestingTaskService _testingTaskService;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<UploadController> _logger;

        private readonly IBus _bus;

        public UploadController(TestingTaskService testingTaskService,
            IBus bus, ILoggerFactory loggerFactory, ILogger<UploadController> logger)
        {
            _testingTaskService = testingTaskService;
            _loggerFactory = loggerFactory;
            _logger = logger;
            _bus = bus;
        }


        [HttpPost("upload/file")]
        public async Task<string> UploadFile(List<IFormFile> postedFile)
        {
            throw new NotImplementedException("Not implemented");
        }

        [HttpPost("{key}", Name = "load/add")]
        public async Task<string> UniversalAdd(
            [FromQuery] string key, [FromBody] string baseRequeste)
        {
            throw new NotImplementedException("Not implemented");
        }

        [HttpPost("{key}", Name = "load/update")]
        public async Task<string> UniversalUpdate(
            [FromQuery] string key, [FromBody] string baseRequeste)
        {
            throw new NotImplementedException("Not implemented");
        }

        // TODO ADD/UPDATE METHODS
        // [HttpPost("load/{*url}")]
        [HttpPost("{key}", Name = "load/new")]
        public async Task<TestingTaskEntity> UniversalUpload(
            [FromQuery] string key, [FromBody] string baseRequeste)
        {
            var date = DateTime.Now;

            var newTask = new TestingTaskEntity()
            {
                Id = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                CreateDate = date,
                UpdateDate = date,
                Comment = "Test",
                TestName = "Test",
                Status = Status.GetOn
            };

            // TODO Save requst

            await _testingTaskService.CreateAsync(newTask);

            await _bus.Publish(new BusNonTypedData() 
            { 
                Key = key,
                Data = baseRequeste,
                TaskId = newTask.Id
            });

            return newTask;
        }

        // TODO в тесты
        [HttpPost("test")]
        public async Task<string> UploadTest([FromRoute] string route)
        {
            var date = DateTime.Now;

            var newTask = new TestingTaskEntity()
            {
                Id = Guid.NewGuid(),
                GroupId = Guid.NewGuid(),
                CreateDate = date,
                UpdateDate = date,
                Comment = "Test",
                TestName = "Test",
                Status = Status.GetOn
            };

            await _testingTaskService.CreateAsync(newTask);

            _logger.LogInformation($"Вызов метода attenuator/phase/shifter/data/test Guid = " + newTask.Id);

            await _bus.Publish(new BusDataTest() { TestingTaskEntityId = newTask.Id });

            return newTask.Id.ToString();
        }

        [HttpGet("task")]
        public async Task<TestingTaskEntity> GetById(Guid id) 
            => await _testingTaskService.GetAsync(id);

        //[HttpGet("group")]
        //public async Task<AttenuatioPhaseShift> GetGroupTest(string id)
        //    => await _attenuatioPhaseShiftServices.GetAsync(id);

    }
}
