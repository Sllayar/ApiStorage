using ApiStorage.Models;

namespace ApiStorage.Models
{
    //public class AttenuatorPhaseShifterCheckProcedureResult: ResultParameterBase<ResultParameter>
    //{
    //    public AttenuatorPhaseShifterCheckProcedureResult()
    //    {
    //        Description = "S Parameters Result";
    //    }

    //    public AttenuatorPhaseShifterCheckProcedureResult(ResultParameter value) : base(value)
    //    {
    //        Description = "S Parameters Result";
    //    }
    //}

    public class AttenuatorPhaseShifterCheckProcedureResult<ResultParameter>
    {
        public string Description { get; set; }

        public AttenuatorPhaseShifterCheckProcedureResult()
        {
            Description = "S Parameters Result";
        }

        public AttenuatorPhaseShifterCheckProcedureResult(ResultParameter value)
        {
            Description = "S Parameters Result";
        }
    }
}