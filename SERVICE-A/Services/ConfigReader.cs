using Configuration.Models;
using Configuration.Repos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Configuration.Interfaces;

namespace Configuration.Services
{
    public class ConfigReader
    {
        private IRepository _repository;
        private string _applicationName;
        private Timer _timer;

        public ConfigReader(string applicationName, string connectionString, int refreshIntervalInMs)
        {
            _repository = new MongoConfig(connectionString);
            _applicationName = applicationName;

            LoadParameters().Wait();

            _timer = new Timer(async _ => await LoadParameters(), null, refreshIntervalInMs, refreshIntervalInMs);
        }

        private ConcurrentDictionary<string, Parameters> _cache = new ConcurrentDictionary<string, Parameters>();

        private async Task LoadParameters()
        {
            try
            {
                var parametersList = await _repository.GetParametersAsync(_applicationName);

                _cache = new ConcurrentDictionary<string, Parameters>(parametersList.ToDictionary(x => x.Name));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Configuration reload failed: {ex.Message}");
            }
        }

        public T GetValue<T>(string key)
        {
            if (!_cache.TryGetValue(key, out var parameter))
            {
                throw new Exception($"'{key}' bulunamadı.");
            }

            return (T)Convert.ChangeType(
                parameter.Value,
                typeof(T));
        }
    }
}
