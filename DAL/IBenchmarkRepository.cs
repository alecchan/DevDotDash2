using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevDotDash2.DAL.Entities;

namespace DevDotDash2.DAL
{
    public interface IBenchmarkRepository : IDisposable
    {
        IEnumerable<Benchmark> GetBenchmarks(DateTime start, DateTime end);

    }
}
