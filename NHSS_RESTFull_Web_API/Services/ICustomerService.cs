using Microsoft.AspNetCore.Mvc; // Make sure to include this namespace
using NHSS_RESTFull_Web_API.Model;

namespace NHSS_RESTFull_Web_API.Services  
{  
    public interface ICustomerService  
    {  
        List<Customer> GetAll(); // This method can remain as is since it doesn't require ActionResult
        ActionResult<Customer> GetById(int id);  
        ActionResult Add(Customer customer);  
        ActionResult Remove(int id);  
    }  
}  