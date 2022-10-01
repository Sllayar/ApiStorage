using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ApiStorage.Models
{
    public class AttenuationPhaseShiftData
    {
        public AttenuationPhaseShiftSettings Settings { get; set; }
        public List<Tuple<double, double>> AmpData { get; set; }
        public List<Tuple<double, double>> PhaseData { get; set; }
    }
}