using System;
using System.Collections.Generic;

namespace DevDotDash2.DAL
{
    public interface IBuildBenchmarkRepository
    {
        IEnumerable<BuildBenchmark> GetBuildBenchmarks(DateTime start, DateTime end);
    }
}