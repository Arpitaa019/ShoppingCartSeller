using ProjectsLibrary.Service.Abstraction.Product;
using ShoppingCart.Services.Service.Client;
using ShoppingCartSeller.DTO.Cart;
using ShoppingCartSeller.DTO.Delivery;
using ShoppingCartSeller.DTO.Discount;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.DTO.Payments;
using ShoppingCartSeller.Services.Abstraction.Cart;
using ShoppingCartSeller.Services.Abstraction.Orders;
using ShoppingCartSeller.Services.Abstraction.Payment;
using ShoppingCartSeller.Services.Abstraction.ProductTypePayment;
using ShoppingCartSeller.Services.Service.Discount;
using System.Text;
using System.Text.Json;

namespace ShoppingCart.Services.Service.Order
{
    public class OrderManagerService : IOrderManagerService
    {
        private readonly IInventoryService _inventory;
        private readonly IPaymentChargeService _paymentChargeService;
        private readonly IFinalAmountCalculatorService _finalAmountCalculatorService;
        private readonly IDiscountCalculatorService _discountCalculatorService;
        private readonly IOrderService _orderService;
        private readonly IEmailClient _emailClient;
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderStatusLogService _orderStatusLogService;

        public OrderManagerService(
            IInventoryService inventory,
            IPaymentChargeService paymentChargeService,
            IFinalAmountCalculatorService finalAmountCalculatorService,
            IDiscountCalculatorService discountCalculatorService,
            IOrderService orderService,
            IEmailClient emailClient,
            IOrderItemService orderItemService,
            IOrderStatusLogService orderStatusLogService)
        {
            _inventory = inventory;
            _paymentChargeService = paymentChargeService;
            _finalAmountCalculatorService = finalAmountCalculatorService;
            _discountCalculatorService = discountCalculatorService;
            _orderService = orderService;
            _emailClient = emailClient;
            _orderItemService = orderItemService;
            _orderStatusLogService = orderStatusLogService;
        }
        public Task<bool> CancelOrderAsync(Guid orderId, string reason)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> GetOrderDetailsAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTO> PlaceOrderAsync(CartOrderCreateDTO request)
        {
            try
            {
                // 1. Validate Cart
                if (request.items == null || !request.items.Any())
                    throw new ArgumentException("Cart is empty.");

                string orderNumber = GenerateOrderNumber();
                List<OrderItemDTO> orderItems = new List<OrderItemDTO>();

                // 2. Map items and assign InventoryID
                foreach (var item in request.items)
                {
                    if (item.Quantity <= 0 || item.UnitPrice <= 0)
                        throw new ArgumentException($"Invalid quantity or price for product {item.ProductName}");

                    int? inventoryId = await _inventory.CheckAvailabilityAsync(item.ProductId, item.Quantity);
                    if (inventoryId == null)
                        throw new InvalidOperationException($"Product {item.ProductName} is not available in inventory.");
                    else
                    {
                        OrderItemDTO orderItemDTO = new OrderItemDTO()
                        {
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            InventoryID = inventoryId.Value,
                            Price = item.UnitPrice,
                            DeliveryStatus = DeliveryStatus.Pending,
                            Title=item.ProductName
                        };
                        orderItems.Add(orderItemDTO);
                    }
                }

                // 3. Calculate Payment
                decimal baseAmount = request.items.Sum(i => i.UnitPrice * i.Quantity);
                decimal platformFee = _paymentChargeService.GetPlatformFee();
                decimal methodFee = _paymentChargeService.GetPaymentMethodCharge("UPI");// use type of payment method


                var discountRules = new List<DiscountRuleDTO>
                {
                    new DiscountRuleDTO { Type = "Amount", MinAmount = 500, DiscountMode = "Percentage", Value = 10 },
                    new DiscountRuleDTO { Type = "PaymentMethod", PaymentMethod = "UPI", DiscountMode = "Percentage", Value = 5 }
                };

                var discountAmount = _discountCalculatorService.CalculateDiscount(request.items, baseAmount, request.paymentInformation, discountRules);
                decimal finalAmount = baseAmount + platformFee + methodFee - discountAmount;

                request.paymentInformation.Amount = finalAmount;
                request.paymentInformation.Status = "Pending";
                request.paymentInformation.PaidAt = DateTime.UtcNow;


                // 4. Create Order
                //var orderId = Guid.NewGuid();
                //var orderNumber = GenerateOrderNumber();

                var orderDTO = new OrderDTO
                {
                    UserId = request.UserId,
                    OrderNumber = orderNumber,
                    Status = OrderStatusDTO.Placed,
                    PlacedAt = DateTime.UtcNow,
                    TotalAmount = finalAmount,
                    Address = request.orderAddressDTO,
                    Offers = request.offerDTOs,
                    items = orderItems
                };

                var createdOrder = await _orderService.CreateOrderAsync(orderDTO);
                // return orderId ;
                //var orderId = 1;
                var orderId = Guid.NewGuid();


                // 5. Insert Order Items
                await _orderService.CreateOrderItemAsync(createdOrder.Id, orderItems);

                foreach (var item in orderItems)
                {
                    await _orderItemService.CreateAsync(item);
                    await _inventory.ReserveAsync(item.InventoryID, item.Quantity);  // blocked quantity method in inv table
                }


                // await _statusLog.LogAsync(orderId, OrderStatus.Placed, "Order created");

                //  Simulate Payment Collection
                bool paymentSuccess = await SimulateThirdPartyPaymentAsync(request.paymentInformation);

               await  _orderService.UpdatePaymentTransaction(orderId, "paymentTransactionId");

                //OrderStatusLogDTO orderStatusLogDTO = new OrderStatusLogDTO();
                // IOrderStatusLogService orderStatusLogService = new OrderStatusLogService();
                //await orderStatusLogService.CreateAsync(orderStatusLogDTO);

                await _orderStatusLogService.CreateAsync(new OrderStatusLogDTO
                {
                    OrderId = createdOrder.Id,
                    Status = createdOrder.Status,
                    ChangedAt = DateTime.UtcNow,
                    Remarks = "Order created",
                });


                // 5. Notifications     
                //generate two type o mail customer and seller and sent that body in notify

                await NotifyCustomerAsync(createdOrder);
                await NotifySellerAsync(createdOrder);

                return createdOrder;
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error in PlaceOrderAsync: {ex.Message}");
                throw; 
            }


        }

        public class OrderPaymentRequest
        {
            public string OrderNumber { get; set; }
            public decimal Amount { get; set; }
            public string PaymentMethod { get; set; }

        }

        private async Task<bool> SimulateThirdPartyPaymentAsync(PaymentInformationDTO payment)
        {

            OrderPaymentRequest opr= new OrderPaymentRequest();
            opr.Amount = payment.Amount;
            opr.OrderNumber=System.Guid.NewGuid().ToString();
            opr.PaymentMethod = "UPI";


            HttpClient httpClient=new HttpClient();
            httpClient.BaseAddress =new Uri("http://localhost:82");
            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Add("AuthenticationKey", "saloni123");
            httpClient.DefaultRequestHeaders.Add("ClientName", "saloni");

            var jsonDate =JsonSerializer.Serialize(opr);
            var content = new StringContent(jsonDate, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("api/Payments", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            // Simulate external payment gateway
            await Task.Delay(1);
            return true;
        }

        private async Task NotifyCustomerAsync(OrderDTO order)
        {
            //await _emailClient.SendEmailAsync();
            // Console.WriteLine($"Notification sent to customer for Order {order.OrderNumber}");
            string htmlBody = MailGenerator.BuildCustomerEmail(order);
            await _emailClient.SendEmailAsync(order.Address.Email, "Your Order Confirmation", htmlBody);
        }

        private async Task NotifySellerAsync(OrderDTO order)
        {
            // await _emailClient.SendEmailAsync();
            // Console.WriteLine($"Notification sent to seller for Order {order.OrderNumber}");
            string htmlBody = MailGenerator.BuildSellerEmail(order);
            await _emailClient.SendEmailAsync("Raghuveer.krishna02@gmail.com", "New Order Received", htmlBody);
        }
        private string GenerateOrderNumber() => $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}";
        public Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusDTO newStatus, string remarks = null)
        {
            throw new NotImplementedException();
        }
    }
} 