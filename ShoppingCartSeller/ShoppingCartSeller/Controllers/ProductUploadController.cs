using Microsoft.AspNetCore.Mvc;
using ProjectsLibrary.Models.Product;
using ProjectsLibrary.Service.Abstraction.Product;
using ProjectsLibrary.Service.Abstraction.ProductType;
using ProjectsLibrary.Service.Services.Product;
using ProjectsLibrary.Shared.Enum;
using ShoppingCartSeller.Core.Entities;
using ShoppingCartSeller.Services.Validations;



namespace ShoppingCart.Controllers
{
    [Route("ProductUpload")]
    public class ProductUploadController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _branservice;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IModelService _modelService;
        private readonly IProductUploadService _productUploadService;
        private readonly IProductExcelReaderService _excelReader;
        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IConfiguration _configuration;
        private readonly PriceCalculatorService _calculator;

        public ProductUploadController(IProductUploadService productUploadService, IProductExcelReaderService excelReader, IProductService productService, 
            IPriceService priceService, ICategoryService categoryService,IBrandService branService, IConfiguration configuration,PriceCalculatorService calculator)
        {
            _productUploadService = productUploadService;
            _excelReader = excelReader;
            _productService = productService;
            _priceService = priceService;
            _branservice = branService;
            _categoryService = categoryService;
            _configuration = configuration;
            _calculator = calculator;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductUploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct()
        {
            return View();
        }

        // GET: ProductUploadController/Create
        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
                return Json(new { success = false, message = "No file uploaded." });

            try
            {
                var uploadedModels = await _excelReader.ParseExcelAsync(excelFile.OpenReadStream());

                ProductValidationEngine engine = new ProductValidationEngine();

                List<(ProductUploadModel, List<string>)> failedProducts = engine.RunValidation(uploadedModels);

              
                foreach (var product in uploadedModels)
                {
                    // if product is in failed list → keep status = New
                    if (failedProducts.Any(fp => fp.Item1 == product))
                    {
                        product.ProductMasterModel.Status = ProductStatus.New;
                    }
                    else
                    {
                        product.ProductMasterModel.Status = ProductStatus.ReadyToReview;   //mark as ReadyToReview
                    }
                }

                await _productUploadService.UploadProduct(uploadedModels);
                return Json(new { success = true, message = $"Uploaded {uploadedModels.Count} products successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Upload failed: {ex.Message}" });
            }
        }

        [HttpGet("Published")]
        public async Task<IActionResult> Published()
        {        
            var products = await _productService.GetAllProductAsync(_calculator);
            return View(products);
        }

        [HttpPost("PublishSelected")]
        public async Task<IActionResult> PublishSelected([FromBody] List<int> productIds)
        {
            try
            {
                foreach (var id in productIds)
                {
                    var product = await _productService.GetProductDetailsAsync(id);
                    if (product != null)
                    {
                        var status = product.ProductMasterModel.Status;

                        if (status != ProductStatus.Published &&
                            status != ProductStatus.Archived &&
                            status != ProductStatus.Unpublished)
                        {
                            await _productService.MarkAsPublishedAsync(id);
                        }
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("ManualProduct")]
        public async Task<IActionResult> ManualProduct()
        {         
            var products = await _productService.GetAllProductAsync(_calculator);
             return View(products);
        }

        [HttpGet("GetProductDetailsJson/{id}")]
        public async Task<IActionResult> GetProductDetailsJson(int id)
        {
            try
            {
                var product = await _productService.GetProductDetailsAsync(id);
                if (product == null)
                    return NotFound(new { success = false, message = "Product not found" });

                return Json(product); // returns JSON for jQuery
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product); // Pass the product model to the view
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductUploadModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _productService.UpdateProductAsync(id, model); 
            return RedirectToAction("ManualProduct");
        }


    }
}
