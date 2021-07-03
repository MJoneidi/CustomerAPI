using CustomerAPI.Models;
using CustomerAPI.Models.Exceptions;
using CustomerAPI.Persistence;
using CustomerAPI.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class CustomerCacheService : ICustomerCacheService
    {
        private List<Customer> customers;
        private readonly IDataHandler _dataHandler;
        private static Mutex mut = new Mutex();

        public CustomerCacheService(IDataHandler dataHandler)
        {
            customers = new List<Customer>();
            _dataHandler = dataHandler;
        }
        public async Task AddAsync(Customer customer)
        {
            await InsertAsync(customer);
        }

        public async Task AddRangeAsync(List<Customer> customers)
        {
            foreach (var item in customers)
                await InsertAsync(item);
        }

        public List<Customer> GetAll()
        {
            return customers;
        }

        public async Task InitiateAsync()
        {
            var data = await _dataHandler.ReadAsync();
            customers.AddRange(data);
        }

        private async Task InsertAsync(Customer customer)
        {
            var validator = new CustomerValidation();
            var failures = validator.Validate(customer);

            if (!failures.IsValid)
                throw new ApiException(string.Join(",", failures.Errors));

            if (customers.Any(rec => rec.ID == customer.ID))
                throw new ApiException("Customer ID is already exist.");



            //so that only one thread at a time can enter.
            mut.WaitOne();
            var index = FindSuitableIndex(customer);
            customers.Insert(index, customer);
            await _dataHandler.WriteAsync(customers);
            mut.ReleaseMutex();
        }

        private int FindSuitableIndex(Customer customer)
        {
            for (int i = 0; i < this.customers.Count; i++)
            {
                var res = string.Compare(customer.LastName, customers[i].LastName);
                if (res < 0)
                    return i;
                if (res > 0)
                    continue;
                else
                {
                    res = string.Compare(customer.FirstName, customers[i].FirstName);
                    if (res < 0)
                        return i;
                    else
                        return i + 1;
                }
            }
            return this.customers.Count;
        }
    }
}
