using Microsoft.AspNetCore.Mvc;  
using Microsoft.Extensions.Logging;  
using NHSS_RESTFull_Web_API.Model;  
using NHSS_RESTFull_Web_API.Services;  
  
[Route("api/[controller]")]  
[ApiController]  
public class CustomerController : ControllerBase  
{  
    private readonly ICustomerService _customerService;  
    private readonly ILogger<CustomerController> _logger;  
  
    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)  
    {  
        _customerService = customerService;  
        _logger = logger;  
    }  
  
    [HttpPost]  
    public IActionResult AddCustomer([FromBody] Customer customer)  
    {  
        if (!ModelState.IsValid)  
        {  
            _logger.LogWarning("Invalid model state for customer: {Customer}", customer);  
            return BadRequest(ModelState);  
        }  
  
        _customerService.Add(customer);  
        _logger.LogInformation("Customer added successfully with ID: {Id}", customer.Id);  
  
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);  
    }  
  
    [HttpDelete("{id}")]  
    public IActionResult RemoveCustomer(int id)  
    {  
        var customer = _customerService.GetById(id);  
        if (customer == null)  
        {  
            _logger.LogWarning("Customer with ID {Id} not found.", id);  
            return NotFound();  
        }  
  
        _customerService.Remove(id);  
        _logger.LogInformation("Customer with ID {Id} removed successfully.", id);  
  
        return NoContent();  
    }  
  
    [HttpGet]  
    public ActionResult<List<Customer>> ListCustomers()  
    {  
        _logger.LogInformation("Retrieving list of all customers.");  
        return Ok(_customerService.GetAll());  
    }  
  
    [HttpGet("{id}")]  
    public ActionResult<Customer> GetCustomer(int id)  
    {  
        var customer = _customerService.GetById(id);  
        if (customer == null)  
        {  
            _logger.LogWarning("Customer with ID {Id} not found.", id);  
            return NotFound();  
        }  
  
        _logger.LogInformation("Retrieved customer with ID {Id}.", id);  
        return Ok(customer);  
    }  
}  