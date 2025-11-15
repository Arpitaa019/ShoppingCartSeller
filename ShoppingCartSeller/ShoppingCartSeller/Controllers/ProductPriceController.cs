using Microsoft.AspNetCore.Mvc;
using ProjectsLibrary.Models.Product;
using ProjectsLibrary.Service.Abstraction.Product;

namespace ShoppingCart.Controllers
{
    [Route("ProductPrice")]
    public class ProductPriceController : Controller
    {
        private readonly IPriceService _priceService;

        public ProductPriceController(IPriceService priceService) 
        {
            _priceService = priceService;
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View(new ProductPriceModel());
        }

        [HttpGet("Prices")]
        public ActionResult Prices()
        {
            return View();
        }

        [HttpPost("Prices")]
        public async Task<IActionResult> Prices(ProductPriceModel model)
        {
            await _priceService.InsertAsync(model);
            return Json(new { message = "Price saved successfully!" });
        }


        [HttpGet("ProductCharges")]
        public ActionResult ProductCharges()
        {
            return View();
        }

        [HttpPost("ProductCharges")]
        public async Task<IActionResult> ProductCharges(ProductChargesModel model)
        {
            await _priceService.ApplyCharges(model);
            return Json(new { message = "Offer saved successfully!" });
        }

        
   
    }
}
