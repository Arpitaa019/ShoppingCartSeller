using Microsoft.AspNetCore.Mvc;
using ShoppingCartSeller.DTO.Seller;
using ShoppingCartSeller.Services.Abstraction.Seller;

namespace ShoppingCartSeller.Controllers
{
    //[Route("Seller")]
    public class SellerController : Controller
    {
        //private readonly ISellerService _service;
        private readonly ISellerDetailsService _service;
        private readonly ICompanyService _companyService;
        private readonly ILoginService _login;

      
        public SellerController(ISellerDetailsService service, ICompanyService companyService, ILoginService login)
        {
            _service = service;
            _companyService = companyService;
            _login = login;
        }

       // [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("LoadDetailsAsync")]
        public async Task<IActionResult> LoadDetailsAsync()
        {
            var sellers = await _service.GetAllSellersAsync();
            var companies = await _companyService.GetAllAsync();
            var loginService = await _login.GetAllLoginAsync();

            return Json(new { success = true, data = new { seller = sellers, company = companies, credentials = loginService } });
        }

        //[HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        //[HttpPost("Create")]
        public async Task<IActionResult> Create(SellerDetailsModel seller)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            try
            {
                var sellerId = await _service.CreateSellerAsync(seller);
                await _companyService.CreateCompanyAsync(seller.Company, sellerId);
                await _login.CreateLoginAsync(seller.Login, sellerId);

                return Json(new { success = true, message = "Seller registered successfully", sellerId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        //[HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.SellerId = id;

            return View();
        }

        //[HttpGet("LoadSellerDetails/{id}")]
        public async Task<IActionResult> LoadSellerDetails(int id)
        {
            var seller = await _service.GetSellerByIdAsync(id);
            return Json(new { success = seller != null, data = new { seller = seller } });
        }


        public async Task<IActionResult> Edit([FromBody] SellerDetailsModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data" });

            var sellerUpdated = await _service.UpdateSellerAsync(model);
            var companyUpdated = await _companyService.UpdateCompanyAsync(model.Company, model.SellerId);
            var loginUpdated = await _login.UpdateLoginAsync(model.Login, model.SellerId);

            if (!sellerUpdated)
                return Json(new { success = false, message = "Seller update failed." });

            if (!companyUpdated)
                return Json(new { success = false, message = "Company update failed." });

            if (!loginUpdated)
                return Json(new { success = false, message = "Login update failed." });

            return Json(new { success = true, message = "Seller, company, and login updated successfully." });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return Json(new { success, message = success ? $"Seller {id} deleted successfully." : $"Delete failed for Seller {id}." });
        }

    }
}
