using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevDotDash2.DAL.Entities
{
    public class TaskResults
    {
        public string Task { get; set; }

        public int Duration { get; set; }

        public List<Result>  Results { get; set; }
    }
}
