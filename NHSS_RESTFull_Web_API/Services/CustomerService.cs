using Microsoft.Extensions.Logging;  
using Microsoft.AspNetCore.Mvc; // Make sure to include this namespace
using NHSS_RESTFull_Web_API.Model;  
  
namespace NHSS_RESTFull_Web_API.Services  
{  
    public class CustomerService : ICustomerService  
    {  
        private readonly List<Customer> customers = new();  
        private int nextId = 1;  
        private readonly ILogger<CustomerService> _logger;  
  
        public CustomerService(ILogger<CustomerService> logger)  
        {  
            _logger = logger;  
        }  
  
        public List<Customer> GetAll()  
        {  
            _logger.LogInformation("Fetching all customers. Total count: {Count}", customers.Count);  
            return customers;  
        }  
  
        public ActionResult<Customer> GetById(int id)  
        {  
            var customer = customers.FirstOrDefault(c => c.Id == id);  
            if (customer == null)  
            {  
                _logger.LogWarning("Customer with ID {Id} not found.", id);  
                return new NotFoundResult(); // Return 404 Not Found
            }  
  
            _logger.LogInformation("Customer with ID {Id} retrieved successfully.", id);  
            return new ActionResult<Customer>(customer); // Return the customer
        }  
  
        public ActionResult Add(Customer customer)  
        {  
            customer.Id = nextId++;  
            customers.Add(customer);  
            _logger.LogInformation("Added new customer: {@Customer}", customer);  
            return new CreatedAtActionResult(nameof(GetById), "Customer", new { id = customer.Id }, customer); // Return 201 Created
        }  
  
        public ActionResult Remove(int id)  
        {  
            var customerResult = GetById(id);  
            if (customerResult.Result is NotFoundResult)  
            {  
                return customerResult.Result; // Return 404 Not Found
            }
            
            var customer = customerResult.Value; 
            customers.Remove(customer);  
            _logger.LogInformation("Removed customer with ID {Id}: {@Customer}", id, customer);  
            return new NoContentResult(); // Return 204 No Content
        }  
    }  
}  