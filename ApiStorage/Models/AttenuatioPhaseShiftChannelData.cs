using System.Collections.Generic;

namespace ApiStorage.Models
{
    public class AttenuatioPhaseShiftChannelData
    {
        public List<AttenuationPhaseShiftData> Parameters { get; set; }
        //public AttenuatorPhaseShifterMeasurementChannelSettings Settings { get; set; }
        public bool FullResult { get; set; }

        public AttenuatioPhaseShiftChannelData()
        {
            Parameters = new List<AttenuationPhaseShiftData>();
            //Settings = new AttenuatorPhaseShifterMeasurementChannelSettings();
            FullResult = false;
        }
    }
}