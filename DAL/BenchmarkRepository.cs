using DevDotDash2.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevDotDash2.DAL
{
    public class BenchmarkRepository : IBenchmarkRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public BenchmarkRepository()
        {
            _client = new MongoDB.Driver.MongoClient("mongodb://dev-srvsql01");
            _db = _client.GetDatabase("benchmark");

        }

        public void Dispose()
        {

        }

        public IEnumerable<Benchmark> GetBenchmarks(DateTime start, DateTime end)
        {
            var collection = _db.GetCollection<Benchmark>("benchmark");

            var result = collection.Find<Benchmark>((x) => x.CreatedDate >= start && x.CreatedDate <= end).SortByDescending(x => x.Duration).ToList();

            return result;
        }

        public IEnumerable<string> GetHosts()
        {
            var collection = _db.GetCollection<Benchmark>("benchmark");
            var aggregate = collection.Aggregate()
                 .Group(
                            x => x.HostName,
                            g => new {
                                Result = g.Select(
                                           x => x.HostName
                                           ).Max()
                            }
                        ).ToList();
            
            var data = aggregate.Select(x => x.Result);
            return data;
        }

        public IEnumerable<Benchmark> GetBenchmarkByHost()
        {
            var collection = _db.GetCollection<Benchmark>("benchmark");
            var hosts = GetHosts();

            foreach(var host in hosts)
            {

            }

            // var result = table.

            return null;
        }
    }
}
