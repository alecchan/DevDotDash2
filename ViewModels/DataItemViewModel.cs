
using System;
using System.Collections.Generic;

public class DataItemViewModel
    {
        public DateTime CreatedDate { get; set; }
        public int? Duration { get; set; }
        public int? CpuDuration { get; set; }
        public int? DiskDuration { get; set; }
        public int? NetworkDuration { get; set; }
        public int? MemoryDuration { get; set; }
    }

    public class HostDataViewModel
    {
        public string HostName { get; set; }
        public List<DataItemViewModel> DataItems { get; set; } = new List<DataItemViewModel>();
    }
