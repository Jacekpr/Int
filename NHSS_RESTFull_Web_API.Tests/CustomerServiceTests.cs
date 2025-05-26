using NHSS_RESTFull_Web_API.Services;  
using NHSS_RESTFull_Web_API.Model;  
using Moq;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.Extensions.Logging;  
  
namespace NHSS_RESTFull_Web_API.Tests  
{  
    public class CustomerControllerTests  
    {  
        private readonly Mock<ICustomerService> _mockCustomerService;  
        private readonly Mock<ILogger<CustomerController>> _mockLogger;  
        private readonly CustomerController _controller;  
  
        public CustomerControllerTests()  
        {  
            _mockCustomerService = new Mock<ICustomerService>();  
            _mockLogger = new Mock<ILogger<CustomerController>>(); // Mock the ILogger  
            _controller = new CustomerController(_mockCustomerService.Object, _mockLogger.Object); // Pass the mock logger  
        }  
  
        [Fact]  
        public void AddCustomer_ReturnsCreatedAtActionResult_WhenModelIsValid()  
        {  
            var customer = new Customer { Firstname = "John", Surname = "Doe" };  
            _mockCustomerService.Setup(service => service.Add(It.IsAny<Customer>()));  
  
            var result = _controller.AddCustomer(customer);  
  
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);  
            Assert.Equal(nameof(_controller.GetCustomer), createdResult.ActionName);  
            Assert.Equal(customer, createdResult.Value);  
        }  
  
        [Fact]  
        public void AddCustomer_ReturnsBadRequest_WhenModelStateIsInvalid()  
        {  
            _controller.ModelState.AddModelError("Firstname", "Required");  
            var customer = new Customer { Surname = "Doe" };  
  
            var result = _controller.AddCustomer(customer);  
  
            Assert.IsType<BadRequestObjectResult>(result);  
        }  
  
        [Fact]  
        public void RemoveCustomer_ReturnsNoContent_WhenCustomerExists()  
        {  
            var customerId = 1;  
            var customer = new Customer { Id = customerId, Firstname = "John", Surname = "Doe" };  
  
            _mockCustomerService.Setup(service => service.GetById(customerId)).Returns(customer);  
            _mockCustomerService.Setup(service => service.Remove(customerId));  
  
            var result = _controller.RemoveCustomer(customerId);  
  
            Assert.IsType<NoContentResult>(result);  
            _mockCustomerService.Verify(service => service.Remove(customerId), Times.Once);  
        }  
    }  
}  