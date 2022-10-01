using ViewStorage.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStorage.Mongo;
using Microsoft.Extensions.Logging;
using CommonStorage.Mongo;

namespace ViewStorage.Controllers
{
    [Route("[controller]")]
    public class AttenuatioPhaseShiftController : Controller
    {
        private readonly AttenuatioPhaseShiftServices _attenuatioPhaseShiftServices;
        private readonly ILogger<AttenuatioPhaseShiftController> _logger;
        private readonly TestingTaskService _testingTaskService;

        public AttenuatioPhaseShiftController(
            TestingTaskService testingTaskService,
            ILogger<AttenuatioPhaseShiftController> logger,
            AttenuatioPhaseShiftServices attenuatioPhaseShiftServices)
        {
            _testingTaskService = testingTaskService;
            _logger = logger;
            _attenuatioPhaseShiftServices = attenuatioPhaseShiftServices;
        }

        [HttpGet("settings")]
        public object GetSettingsInMemmory(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_attenuatioPhaseShiftServices.GetQuery(), loadOptions);
        }

        [HttpGet("result")]
        public object GetResult(DataSourceLoadOptions loadOptions, Guid id)
        {
            return _attenuatioPhaseShiftServices.GetResult(id)?.Parameters;
        }
    }
}
