using DevDotDash2.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevDotDash2.DAL
{
    public class BuildBenchmarkRepository : IBuildBenchmarkRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public BuildBenchmarkRepository()
        {
            _client = new MongoDB.Driver.MongoClient("mongodb://dev-srvsql01");
            _db = _client.GetDatabase("benchmark");

        }

        public void Dispose()
        {

        }

        public IEnumerable<BuildBenchmark> GetBuildBenchmarks(DateTime start, DateTime end)
        {
            var collection = _db.GetCollection<BuildBenchmark>("buildbenchmark");

            var result = collection.Find<BuildBenchmark>((x) => x.CreatedDate >= start && x.CreatedDate <= end).ToList();

            return result;
        }

       
    }
}
