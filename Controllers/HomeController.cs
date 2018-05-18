using System;
using System.Collections.Generic;
using System.Linq;
using DevDotDash2;
using DevDotDash2.DAL;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

public class HomeController : Controller {
    public HomeController () {

    }

    public IActionResult Index () {
        return View ();
    }

    public IActionResult Data () {

        var repo = new BenchmarkRepository ();
        var raw = repo.GetBenchmarks (DateTime.Now.StartOfWeek (DayOfWeek.Monday), DateTime.Now);
        //var x = repo.GetHosts ();
        return Json(raw);

        //  var hosts = repo.GetHosts().OrderBy(x => x);
        //     var dates = raw.Select(x => x.CreatedDate).Distinct().OrderBy(x=>x);
        //     var data = new List <object[]>();
        //     foreach(var d in dates)
        //     {
        //         List<object> itemData = new List<object>();
        //         itemData.Add(d);
        //         foreach(var h in hosts)
        //         {
        //             var item = raw.FirstOrDefault(x => x.HostName == h && x.CreatedDate == d);
        //             itemData.Add(item == null ? null : (item.Duration / 1000).ToString());
        //         }

        //         data.Add(itemData.ToArray());
        //     }
        //     return Json(data.ToArray());
    }
}