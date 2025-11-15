
namespace ShoppingCartSeller.Infrastructure.Sql
{
    public class SellerSql
    {
        public const string InsertSellerDetails = "sp_InsertSellerDetails";
        public const string InsertCompany = "sp_InsertCompany";
        public const string InsertSellerLogin = "sp_InsertSellerLogin";

        public const string UpdateSellerDetails = "sp_UpdateSellerDetails";
        public const string UpdateCompanyDetails = "sp_UpdateCompanyDetails";
        public const string UpdateSellerLoginDetails = "sp_UpdateSellerLogins";

        public const string DeleteSellerById = "sp_DeleteSellerById";
        public const string DeleteCompanyBySellerId = "sp_DeleteCompanyBySellerId";

        public const string GetAllSellerDetails = "sp_GetAllSellerDetails";
        public const string GetAllCompanies = "sp_GetAllCompanies";
        public const string GetAllSellerLogins = "sp_GetAllSellerLogins";
        public const string GetSellerDetailsById = "sp_GetSellerDetailsById";

        public static string GetLoginBySellerId { get; internal set; }
  
        public const string InsertOrder = "sp_InsertOrder";
        public const string InsertOrderItem = "sp_InsertOrderItem";
        public const string GetOrderById = "sp_GetOrderById";
        public const string GetAllOrders = "sp_GetAllOrders";

        public const string UpdatePaymentTransaction = "sp_UpdatePaymentTransaction";

        public const string GetCustomerInteractions = "sp_GetCustomerInteractionsWithProduct";

        public const string AddSellerNotification = "sp_AddSellerNotification";
        public const string GetAllSellerNotifications = "sp_GetAllSellerNotifications";
        public const string GetUnreadSellerNotifications = "sp_GetUnreadSellerNotifications";
        public const string GetSellerLatestNotification = "sp_GetSellerLatestNotification";
        public const string MarkNotificationsAsRead = "sp_MarkNotificationsAsRead";
        public const string MarkSellerNotificationAsRead = "sp_MarkSellerNotificationAsRead";

    }
}
