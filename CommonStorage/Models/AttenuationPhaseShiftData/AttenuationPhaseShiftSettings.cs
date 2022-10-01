namespace ApiStorage.Models
{
    public class AttenuationPhaseShiftSettings
    {
        public AttenuationPhaseShiftSettings()
        {
        }

        public AttenuationPhaseShiftSettings(double attenuationValue, double phaseShiftValue)
        {
            AttenuationValue = attenuationValue;
            PhaseShiftValue = phaseShiftValue;
        }

        public double AttenuationValue { get; set; }
        public double PhaseShiftValue { get; set; }
    }
}