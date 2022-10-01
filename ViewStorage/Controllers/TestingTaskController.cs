using CommonStorage.Mongo;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewStorage.Controllers
{
    [Route("[controller]")]
    public class TestingTaskController : Controller
    {
        private readonly TestingTaskService _testingTaskService;

        public TestingTaskController(TestingTaskService testingTaskService)
        {
            _testingTaskService = testingTaskService;
        }

        [HttpGet("testing")]
        public object GeTestingTask(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_testingTaskService.GetQuery(), loadOptions);
        }
    }
}
