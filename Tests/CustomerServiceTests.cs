using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {

        //[Fact]
        //public async void CanPurchase()
        //{
        //    var mockSet = new Mock<DbSet<Customer>>();
        //    var mockContext = new Mock<TestDbContext>();

        //    mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

        //    var service = new CustomerService(mockContext.Object);


        //    await service.CanPurchase(1,10);

        //    mockSet.Verify(m => m.FindAsync(It.IsAny<Customer>()), Times.Once());
        //    //mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}
    }
}
