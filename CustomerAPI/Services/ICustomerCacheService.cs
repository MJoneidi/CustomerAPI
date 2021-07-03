using CustomerAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface ICustomerCacheService
    {
        Task InitiateAsync();
        Task AddAsync(Customer customer);
        Task AddRangeAsync(List<Customer> customers);
        List<Customer> GetAll();
    }
}
