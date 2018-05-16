using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

public class HomeController : Controller
{
    public HomeController()
    {

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Data()
    {
        var text = System.IO.File.ReadAllText("data.bson");

        var data = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(text);

        
        return Content(data.ToJson(), "Application/Json");
    }
}