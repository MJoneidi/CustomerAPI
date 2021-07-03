using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Configurations
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration(IConfiguration configuration)
        {
            this.FilePath = configuration["FilePath"];
        }

        public string FilePath { get; set; }
    }
}
