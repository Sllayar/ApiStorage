using System;
using System.Collections.Generic;
using System.Text;

namespace ApiStorage.Models.Bus
{
    public class BusNonTypedData
    {
        public Guid TaskId { get; set; }

        public string Key { get; set; }

        public string Data { get; set; }
    }
}
