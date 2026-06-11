using Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Configuration.Interfaces;

namespace Configuration.Repos
{
    public class MongoConfig : IRepository
    {
        private IMongoCollection<Parameters> _parametersCollection;

        public MongoConfig(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ConfigDB");
            _parametersCollection = database.GetCollection<Parameters>("Parameters");
        }

        public async Task<List<Parameters>> GetParametersAsync(string applicationName)
        {
            return await _parametersCollection.Find(x => x.ApplicationName == applicationName && x.IsActive).ToListAsync();
        }
    }
}
