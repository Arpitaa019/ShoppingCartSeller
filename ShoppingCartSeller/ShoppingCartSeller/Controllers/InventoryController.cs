using Microsoft.AspNetCore.Mvc;
using ProjectsLibrary.Service.Abstraction.Product;

namespace ShoppingCartSeller.Controllers
{

    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController( IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public  async Task<IActionResult> GetDummyInventory()
        {

          var dummy = await  _inventoryService.FetchAllStocks();


            return Json(new { success = true, data = dummy });
        }

    }
}
