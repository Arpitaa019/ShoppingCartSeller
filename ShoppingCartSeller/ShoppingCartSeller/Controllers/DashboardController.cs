using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using ProjectsLibrary.Models.Product;
using ProjectsLibrary.Service.Abstraction.Product;
using ProjectsLibrary.Service.Services.Product;
using ShoppingCartSeller.Core.Entities;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Services.Abstraction.Orders;

namespace ShoppingCartSeller.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly PriceCalculatorService _calculator;
        public DashboardController(IOrderService orderService, IProductService productService, IConfiguration configuration, PriceCalculatorService calculator)
        {
            _productService = productService;
            _orderService = orderService;
            _configuration = configuration;
            _calculator = calculator;
        }
        public async Task<IActionResult> Index()
        {
        
            var products = await _productService.GetAllProductAsync( _calculator);
            var orders = await _orderService.GetAllOrdersAsync();

            ViewBag.TotalOrders = orders?.Count();
            ViewBag.PendingOrders = orders?.Count(o => o.Status == OrderStatusDTO.Pending);
            ViewBag.DeliveredOrders = orders?.Count(o => o.Status == OrderStatusDTO.Delivered);

            return View(products ?? new List<ProductUploadModel>());
        }
        public async Task<IActionResult> LoadProductDetails()
        {
            try
            {
                var products = await _productService.GetAllProductAsync( _calculator);
                return Json(new { success = products != null, data = new { products } });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Server error: {ex.Message}" });
            }
        }

        public IActionResult UploadedProduct()
        {
            return View();
        }
        public IActionResult Setting()
        {
            return View();
        }
       
        public IActionResult Logout()
        {
            return View();
        }
    }
}
