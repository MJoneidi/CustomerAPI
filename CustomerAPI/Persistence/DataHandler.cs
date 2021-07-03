using CustomerAPI.Configurations;
using CustomerAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Persistence
{
    public class FileHandler : IDataHandler
    {
        private readonly ILogger<FileHandler> _logger;
        private readonly IApplicationConfiguration _applicationConfiguration;
      
        public FileHandler(ILogger<FileHandler> logger, IApplicationConfiguration applicationConfiguration)
        {
            _logger = logger;
            _applicationConfiguration = applicationConfiguration;
        }

        public async Task<IEnumerable<Customer>> ReadAsync()
        {
            try
            {
                var allText = await System.IO.File.ReadAllTextAsync(_applicationConfiguration.FilePath);
                return JsonConvert.DeserializeObject<List<Customer>>(allText);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task WriteAsync(IEnumerable<Customer> customers)
        {
            try
            {
                var json = JsonConvert.SerializeObject(customers);
                await System.IO.File.WriteAllTextAsync(_applicationConfiguration.FilePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
