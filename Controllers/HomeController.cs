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

    //public IActionResult Data () {

    //    var repo = new BenchmarkRepository ();
    //    var raw = repo.GetBenchmarks (DateTime.Now.StartOfWeek (DayOfWeek.Monday), DateTime.Now);
    //    //var x = repo.GetHosts ();
    //    return Json(raw);

    //    //  var hosts = repo.GetHosts().OrderBy(x => x);
    //    //     var dates = raw.Select(x => x.CreatedDate).Distinct().OrderBy(x=>x);
    //    //     var data = new List <object[]>();
    //    //     foreach(var d in dates)
    //    //     {
    //    //         List<object> itemData = new List<object>();
    //    //         itemData.Add(d);
    //    //         foreach(var h in hosts)
    //    //         {
    //    //             var item = raw.FirstOrDefault(x => x.HostName == h && x.CreatedDate == d);
    //    //             itemData.Add(item == null ? null : (item.Duration / 1000).ToString());
    //    //         }

    //    //         data.Add(itemData.ToArray());
    //    //     }
    //    //     return Json(data.ToArray());
    //}

    public class DataItem
    {
        public DateTime CreatedDate { get; set; }
        public int? Duration { get; set; }
        public int? CpuDuration { get; set; }
        public int? DiskDuration { get; set; }
        public int? NetworkDuration { get; set; }
        public int? MemoryDuration { get; set; }
    }

    public class HostData
    {
        public string HostName { get; set; }
        public List<DataItem> DataItems { get; set; } = new List<DataItem>();
    }

    public IActionResult Data()
    {
    
        var benchmarks = _repo.GetBenchmarks(DateTime.Now.StartOfWeek(DayOfWeek.Saturday), DateTime.Now);

        var hosts = benchmarks.GroupBy(g => g.HostName)
                              .Select(s => new { HostName = s.Key, Count = s.Count() })
                              .OrderByDescending(x => x.Count)
                              .Select(s => s.HostName);

        var dates = benchmarks.Select(x => x.CreatedDate).Distinct().OrderBy(x => x);

        var hostData = new List<HostData>();

        foreach (var h in hosts)
        {
            var item = new HostData { HostName = h };
            hostData.Add(item);

            foreach (var d in dates)
            {
                var dataItem = new DataItem { CreatedDate = d };
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