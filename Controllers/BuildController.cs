using System;
using System.Collections.Generic;
using System.Linq;
using DevDotDash2;
using DevDotDash2.DAL;
using DevDotDash2.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

public class BuildData {

    public string ProjectName { get; set; }

    public string Host { get; set; }

    public DateTime CreatedDate { get; set; }

    public int Duration { get; set; }
}

public class BuildController : Controller {

    private readonly IBuildBenchmarkRepository _repo;
    public BuildController (IBuildBenchmarkRepository repo) {
        _repo = repo;
    }

    public IActionResult Index () {

        return View ();
    }

    [Produces ("application/json")]
    public IActionResult Data () {

        var benchmarks = _repo.GetBuildBenchmarks (DateTime.Now.StartOfWeek (DayOfWeek.Saturday), DateTime.Now)
                    .Where (x => x.SolutionBuild == false && x.UpToDate == false );

        var projects = benchmarks            
            .GroupBy (g => g.Tags)
            .Select (s => new { Tags = s.Key, Count = s.Count () })
            .OrderByDescending (x => x.Count)
            .Select (s => s.Tags);

        var data = new Dictionary<string, List<BuildData>>();

        foreach (var p in projects) {
            
            var items = benchmarks.Where(x=>x.Tags == p ).Select(x=> new BuildData {
                ProjectName = x.Tags,
                Host = x.Host,
                Duration = x.BuildDuration,
                CreatedDate = x.CreatedDate
            });
            
            data.Add(p, items.ToList());
        }

        return Json (data.ToArray ());
    }
}