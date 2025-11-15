using Microsoft.AspNetCore.Mvc;
using ShoppingCartSeller.DTO.Cart;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.DTO.Payments;
using ShoppingCartSeller.Services.Abstraction.Orders;

namespace ShoppingCartSeller.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManagerService orderManagerService;
        private readonly IOrderService _orderService;
        public OrderController(IOrderManagerService orderManager, IOrderService orderService)
        {
            orderManagerService = orderManager;
            _orderService = orderService;

        }

        public IActionResult OrderManagement()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Json(new { success = true, data = orders });
        }

        public async Task<IActionResult> GetOrderDetails(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return Json(new { success = false, message = "Order not found." });

            return Json(new { success = true, data = order });
        }

        public async Task<IActionResult> RequestRefund([FromForm] Guid orderId, [FromForm] string reason)
        {
            var success = await _orderService.CancelOrderAsync(orderId, reason);
            return Json(new { success, message = success ? "Refund processed." : "Refund failed." });
        }

        public async Task<IActionResult> GetInvoice(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return Content("Invoice not available.");

            return PartialView("Invoice", order); // Uses Views/Order/Invoice.cshtml
        }

        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderSummary()
        {
            return View();
        }

        public ActionResult OrderDetails()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> OrderReport()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        //public ActionResult CreateByProduct(ProductOrderCreateDTO productOrderCreateDTO)
        //{
        //    return View();
        //}

        public ActionResult CreateByCartTest()
        {
            orderManagerService.PlaceOrderAsync(GetSampleCartOrder());
            return Content(" Test order placed successfully. Check your email notification.");
            //return View();
        }


        public static CartOrderCreateDTO GetSampleCartOrder()
        {
            var testCartOrder = new CartOrderCreateDTO
            {
                UserId = "user-123",   //

                items = new List<CartItemDTO>
                {
                    new CartItemDTO
                    {
                        ProductId = 432,
                        ProductName = "Bluetooth Headphones",
                        Quantity = 1,
                        UnitPrice = 233.00m
                    }
                },

                orderAddressDTO = new OrderAddressDTO
                {
                    Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                    OrderId = Guid.Parse("66666666-7777-8888-9999-000000000000"),
                    RecipientName = "Raghuveer",
                    ContactNumber = "9876543210",
                    AddressLine = "Flat 203, Green Residency",
                    City = "Mulshi",
                    State = "Maharashtra",
                    PostalCode = "412108",
                    Email = "jaiswalarpita109@gmail.com"
                },

                paymentInformation = new PaymentInformationDTO
                {
                    TransactionId = Guid.Parse("99999999-aaaa-bbbb-cccc-dddddddddddd"),
                    PaymentMethod = "UPI",
                    Amount = 290.00m,
                    PaidAt = new DateTime(2025, 10, 12, 11, 45, 00),
                    Status = "Success",
                    Remarks = "5% cashback applied via UPI offer",

                    Upi = new UpiDetailsDTO
                    {
                        UpiId = "raghuveer@upi",
                        VerifiedBy = "PhonePe"
                    },

                    Card = new CardDetailsDTO
                    {
                        CardType = "Credit",
                        Last4Digits = "1234",
                        Issuer = "HDFC"
                    },

                    NetBanking = new NetBankingDetailsDTO
                    {
                        BankName = "ICICI Bank",
                        ReferenceNumber = "NBK202510120001"
                    },

                 
                    EMI = new EmiDetailsDTO
                    {
                        Provider = "Bajaj Finserv",
                        TenureMonths = 6,
                        MonthlyInstallment = 500.00m
                    },

                    COD = new CodDetailsDTO
                    {
                        DeliveryAgent = "Ekart Logistics",
                        ExpectedCollectionDate = new DateTime(2025, 10, 15)
                    }
                }
            };
            return testCartOrder;
        }
    }
}
