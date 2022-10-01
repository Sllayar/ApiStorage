using System.Collections.Generic;

namespace ApiStorage.Models
{
    public class ResultParameter : ITestData
    {
        public List<AttenuatioPhaseShiftChannelData> Parameters { get; set; }

        public ResultParameter()
        {
            Parameters = new List<AttenuatioPhaseShiftChannelData>();
        }
    }
}