using Moq;
using ProvaPub.Data.Models;
using ProvaPub.Data.Repository;
using ProvaPub.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _customerService = new CustomerService(_mockCustomerRepository.Object, _mockOrderRepository.Object);
        }

        [Fact]
        public async Task CanPurchase_ThrowsException_WhenCustomerIdIsInvalid()
        {
            
            int invalidCustomerId = 0;
            decimal purchaseValue = 50;

            
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(invalidCustomerId, purchaseValue));
        }

        [Fact]
        public async Task CanPurchase_ThrowsException_WhenPurchaseValueIsInvalid()
        {
            
            int customerId = 1;
            decimal invalidPurchaseValue = 0;

            
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(customerId, invalidPurchaseValue));
        }

        [Fact]
        public async Task CanPurchase_ThrowsException_WhenCustomerDoesNotExist()
        {
           
            int customerId = 1;
            decimal purchaseValue = 50;

            _mockCustomerRepository.Setup(repo => repo.FindAsync(customerId)).ReturnsAsync((Customer?)null);

            
            await Assert.ThrowsAsync<InvalidOperationException>(() => _customerService.CanPurchase(customerId, purchaseValue));
        }

        [Fact]
        public async Task CanPurchase_ReturnsFalse_WhenCustomerHasPurchasedThisMonth()
        {
            
            int customerId = 1;
            decimal purchaseValue = 50;

            _mockCustomerRepository.Setup(repo => repo.FindAsync(customerId)).ReturnsAsync(new Customer { Id = customerId });
            _mockOrderRepository.Setup(repo => repo.CountAsync(customerId, It.IsAny<DateTime>())).ReturnsAsync(1);

            
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

           
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_ReturnsFalse_WhenFirstPurchaseExceedsLimit()
        {
            
            int customerId = 1;
            decimal purchaseValue = 150;

            _mockCustomerRepository.Setup(repo => repo.FindAsync(customerId)).ReturnsAsync(new Customer { Id = customerId });
            _mockCustomerRepository.Setup(repo => repo.CountAsync(customerId)).ReturnsAsync(0);

            
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_ReturnsFalse_WhenOutsideBusinessHours()
        {
            
            int customerId = 1;
            decimal purchaseValue = 50;

            _mockCustomerRepository.Setup(repo => repo.FindAsync(customerId)).ReturnsAsync(new Customer { Id = customerId });
            _mockCustomerRepository.Setup(repo => repo.CountAsync(customerId)).ReturnsAsync(1);

            // Simulate outside business hours
            var outsideBusinessHours = new DateTime(2023, 10, 1, 19, 0, 0); 
            SystemTime.Now = () => outsideBusinessHours;

            
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_ReturnsTrue_WhenAllConditionsAreMet()
        {
            
            int customerId = 1;
            decimal purchaseValue = 50;

            _mockCustomerRepository.Setup(repo => repo.FindAsync(customerId)).ReturnsAsync(new Customer { Id = customerId });
            _mockCustomerRepository.Setup(repo => repo.CountAsync(customerId)).ReturnsAsync(1);
            _mockOrderRepository.Setup(repo => repo.CountAsync(customerId, It.IsAny<DateTime>())).ReturnsAsync(0);

            // Simulate business hours
            var businessHours = new DateTime(2023, 10, 1, 10, 0, 0); 
            SystemTime.Now = () => businessHours;

            
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            
            Assert.True(result);
        }
    }

    //  mock DateTime.Now
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}