using CustomerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Persistence
{
    public interface IDataHandler
    {
        Task WriteAsync(IEnumerable<Customer> customers);
        Task<IEnumerable<Customer>> ReadAsync();
    }
}
