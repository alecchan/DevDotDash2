using System;
using System.Collections.Generic;
using System.Linq;
using DevDotDash2;
using DevDotDash2.DAL;
using DevDotDash2.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

public class HomeController : Controller {
    private readonly IBenchmarkRepository _repo;

    public HomeController (IBenchmarkRepository repo) {
        _repo = repo;
    }

    public IActionResult Index () {
        return View ();
    }

    public IActionResult Data(DateTime? start = null, DateTime? end = null) 
    {
        start = start ?? DateTime.Now.StartOfWeek (DayOfWeek.Saturday);
        end = end ?? DateTime.Now;

        var benchmarks = _repo.GetBenchmarks(start.Value, end.Value);

        var hosts = benchmarks.GroupBy(g => g.HostName)
                              .Select(s => new { HostName = s.Key, Count = s.Count() })
                              .OrderByDescending(x => x.Count)
                              .Select(s => s.HostName);

        var dates = benchmarks.Select(x => x.CreatedDate).Distinct().OrderBy(x => x);

        var hostData = new List<HostDataViewModel>();

        foreach (var h in hosts)
        {
            var item = new HostDataViewModel { HostName = h };
            hostData.Add(item);

            foreach (var d in dates)
            {
                var dataItem = new DataItemViewModel { CreatedDate = d };
                item.DataItems.Add(dataItem);

                var benchmark = benchmarks.FirstOrDefault(x => x.HostName == h && x.CreatedDate == d);

                if (benchmark != null)
                {
                    dataItem.Duration = benchmark.Duration / 1000;
                    dataItem.CpuDuration = benchmark.TaskResults.FirstOrDefault(f => f.Task == "CPU")?.Duration / 1000;
                    dataItem.DiskDuration = benchmark.TaskResults.FirstOrDefault(f => f.Task == "Disk[C:\\temp\\Benchmark]")?.Duration / 1000;
                    dataItem.NetworkDuration = benchmark.TaskResults.FirstOrDefault(f => f.Task == "Disk[\\\\dev-srvapp01\\Temporary Files\\Benchmark]")?.Duration / 1000;
                    dataItem.MemoryDuration = benchmark.TaskResults.FirstOrDefault(f => f.Task == "Memory")?.Duration / 1000;
                }
            }            
        }

        return Json(hostData.ToArray());
    }

}