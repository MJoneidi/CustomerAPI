using CustomerAPI.Models;
using CustomerAPI.Models.Exceptions;
using CustomerAPI.Persistence;
using CustomerAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerTests
{
    [TestClass]
    public class CustomerCacheServiceTests
    {
        private Mock<IDataHandler> _dataHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            _dataHandler = new Mock<IDataHandler>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dataHandler.VerifyAll();          
        }


        [TestMethod]
        public async Task ValidRequest_Success()
        {
            // Arrange
            var customerCacheService = new CustomerCacheService(_dataHandler.Object);
            var request = FakeRequest();
            var response = FakeResponse();

            // Act
            await customerCacheService.AddRangeAsync(request);
            var result = customerCacheService.GetAll();

            //Assert
            Assert.AreEqual(result.Count, response.Count);
            _dataHandler.Verify(x => x.WriteAsync(request), Times.AtLeastOnce);            
        }



        [TestMethod]
        public async Task ValidRequest_NotSuccess()
        {
            // Arrange
            var customerCacheService = new CustomerCacheService(_dataHandler.Object);
            var request = FakeBadRequest();
            ;

            // Act

            var exceptionThrown = await Assert.ThrowsExceptionAsync<ApiException>(async () =>
            {
                await customerCacheService.AddRangeAsync(request);
            });

            //Assert
            Assert.IsNotNull(exceptionThrown.Message);
            _dataHandler.Verify(x => x.WriteAsync(request), Times.Never);
        }


        private List<Customer> FakeResponse()
        {
            return new List<Customer>()
                {
                    new Customer() { Age= 56, FirstName= "Aaaa", LastName= "Aaaa", ID=2 },
                    new Customer() { Age= 32, FirstName= "Aaaa", LastName= "Cccc", ID=5 },
                    new Customer() { Age= 50, FirstName= "Bbbb", LastName= "Cccc", ID=1 },
                    new Customer() { Age= 70, FirstName= "Aaaa", LastName= "Dddd", ID=4 }
                };
        }

        private List<Customer> FakeRequest()
        {
            return new List<Customer>()
                {
                    new Customer() { Age= 56, FirstName= "Aaaa", LastName= "Aaaa", ID=2 },
                    new Customer() { Age= 32, FirstName= "Aaaa", LastName= "Cccc", ID=5 },
                    new Customer() { Age= 50, FirstName= "Bbbb", LastName= "Cccc", ID=1 },
                    new Customer() { Age= 70, FirstName= "Aaaa", LastName= "Dddd", ID=4 }
                };
        }

        private List<Customer> FakeBadRequest()
        {
            return new List<Customer>()
                {
                    new Customer() { Age= 16, FirstName= "Aaaa", LastName= "", ID=0 },
                    new Customer() { Age= 32, FirstName= "Aaaa", LastName= "Cccc", ID=5 },
                    new Customer() { Age= 50, FirstName= "Bbbb", LastName= "Cccc", ID=1 },
                    new Customer() { Age= 70, FirstName= "Aaaa", LastName= "Dddd", ID=4 }
                };
        }
    }
}
