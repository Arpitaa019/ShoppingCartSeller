using Microsoft.AspNetCore.Mvc;
using ProjectsLibrary.Models.Product;
using ProjectsLibrary.Service.Abstraction.Product;

namespace ShoppingCart.Controllers
{
    [Route("Offer")]
    public class OfferController : Controller
    {
        private readonly IPriceService _priceService;
        public OfferController(IPriceService priceService)
        {
            _priceService = priceService ;
        }
        public IActionResult Index()
        {
            //test check in rule
            return View();
        }

        [HttpGet("ProductOffer")]
        public ActionResult ProductOffer()
        {
            return View();
        }

        [HttpPost("ProductOffer")]
        public async Task<IActionResult> ProductOffer(ProductOfferModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            await _priceService.SetProductOffers(model);
            return Json(new { message = "Offer saved successfully!" });
        }

    }
}
