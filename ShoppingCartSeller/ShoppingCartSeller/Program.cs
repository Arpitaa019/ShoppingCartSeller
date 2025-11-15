using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsLibrary.Service;
using ProjectsLibrary.Service.Abstraction.Product;
using ProjectsLibrary.Service.Services.Product;
using ShoppingCart.Abstraction.Orders;
using ShoppingCart.Services.Service.Order;
using ShoppingCartSeller.Core.Repository.Abstraction.Customers;
using ShoppingCartSeller.Core.Repository.Abstraction.Notifications;
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Repository.Customers;
using ShoppingCartSeller.Infrastructure.Repository.Notifications;
using ShoppingCartSeller.Infrastructure.Repository.Seller;
using ShoppingCartSeller.Infrastructure.Sql;
using ShoppingCartSeller.Services.Abstraction.Cart;
using ShoppingCartSeller.Services.Abstraction.Customers;
using ShoppingCartSeller.Services.Abstraction.Notifications;
using ShoppingCartSeller.Services.Abstraction.Orders;
using ShoppingCartSeller.Services.Abstraction.Payment;
using ShoppingCartSeller.Services.Abstraction.ProductTypePayment;
using ShoppingCartSeller.Services.Abstraction.Seller;
using ShoppingCartSeller.Services.Service.Client;
using ShoppingCartSeller.Services.Service.Customers;
using ShoppingCartSeller.Services.Service.Discount;
using ShoppingCartSeller.Services.Service.Notification;
using ShoppingCartSeller.Services.Service.Payment;
using ShoppingCartSeller.Services.Service.Sellers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ShoppingCartSellerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddProjectsLibraryServices();
builder.Services.AddSingleton<DbHelper>();

builder.Services.AddScoped<ISellerDetailsRepository, SellerDetailsRepository>();
builder.Services.AddScoped<ISellerDetailsService, SellerDetailsService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPaymentChargeService, PaymentChargeService>();
builder.Services.AddScoped<IFinalAmountCalculatorService, FinalAmountCalculatorService>();
builder.Services.AddScoped<IDiscountCalculatorService, DiscountCalculatorService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IEmailClient, EmailClient>();
builder.Services.AddScoped<IOrderManagerService, OrderManagerService>();
builder.Services.AddScoped<IOrderStatusLogService, OrderStatusLogService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductUploadService, ProductUploadService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationServices, NotificationService>();

builder.Services.AddAuthorization();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new {
                Field = x.Key,
                Messages = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
            });

        return new JsonResult(new
        {
            success = false,
            message = "Model binding failed",
            errors = errors
        });
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
