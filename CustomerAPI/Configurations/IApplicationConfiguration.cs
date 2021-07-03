using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Configurations
{
    /// <summary>
    /// Abstraction to hide real source of configuration
    /// </summary>
    public interface IApplicationConfiguration
    {
        string FilePath { get; set; }
    }
}
