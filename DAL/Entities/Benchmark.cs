using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevDotDash2.DAL.Entities
{
    public class Benchmark
    {
        public object _id { get; set; }

        public string HostName { get; set; }

        public int Duration { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<TaskResults> TaskResults { get; set; }

    }
}
