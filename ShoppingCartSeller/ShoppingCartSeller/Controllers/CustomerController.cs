using Microsoft.AspNetCore.Mvc;
using ShoppingCartSeller.Services.Abstraction.Customers;

namespace ShoppingCartSeller.Controllers
{
    public class CustomerController : Controller
    {       
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) 
        {
           
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult CustomerInteraction()
        {
            return View();
        }

        public async Task<IActionResult> GetInteractions()
        {
            var data = await _customerService.GetAllInteractionsAsync();
            return Json(new { success = true, data });
        }

    }
}
