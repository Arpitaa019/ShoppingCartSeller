

using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCart.Services.Service.Client
{
    public static class MailGenerator
    {
        public static string BuildCustomerEmail(OrderDTO order)
        {
            var address = order.Address;
            var deliveryDate = order.PlacedAt.AddDays(4).ToString("dd MMM yyyy");

            string itemsHtml = string.Join("", order.items.Select(item => $@"
                        <tr>
                            <td>{item.Title}</td>
                            <td>{item.Seller}</td>
                            <td>{item.Quantity}</td>
                            <td>₹{item.Price:0.00}</td>
                            <td>{deliveryDate}</td>
                        </tr>"));

            return $@"
        <html>
        <head>
            <style>
                body {{ font-family: Arial; color: #333; }}
                h2, h3 {{ color: #0073e6; }}
                table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
                th, td {{ padding: 10px; border: 1px solid #ddd; text-align: left; }}
                th {{ background-color: #f5f5f5; }}
                .footer {{ margin-top: 30px; font-size: 0.9em; color: #666; }}
            </style>
        </head>
        <body>
            <h2>Hi {address.RecipientName},</h2>
            <p>Your order <strong>{order.OrderNumber}</strong> was placed on <strong>{order.PlacedAt:dd MMM yyyy}</strong>.</p>

            <h3>Delivery Address:</h3>
            <p>{address.AddressLine}, {address.City}, {address.State} - {address.PostalCode}</p>

            <h3>Order Summary:</h3>
            <table>
                <thead>
                    <tr><th>Product</th><th>Seller</th><th>Qty</th><th>Price</th><th>Delivery Date</th></tr>
                </thead>
                <tbody>{itemsHtml}</tbody>
            </table>

            <h3>Payment Summary:</h3>
            <p><strong>Total Paid:</strong> ₹{order.TotalAmount}</p>
            <p><strong>SuperCoins Used:</strong> 0</p>
            <p><strong>SuperCoins Earned:</strong> 10</p>

            <p class='footer'>Thank you for shopping with us!<br/>Need help? Contact our 24x7 Customer Care.</p>
        </body>
        </html>";
        }

        public static string BuildSellerEmail(OrderDTO order)
        {
            var address = order.Address;

            string itemsHtml = string.Join("", order.items.Select(item => $@"
                        <tr>
                            <td>{item.Title}</td>
                            <td>{item.Quantity}</td>
   <td> <img border=""0"" src=""https://ci3.googleusercontent.com/meips/ADKq_NZoRTlfePhyb7axWZ1e-sOYOIqCMpa8nFOVKXFXfBtTBX7IzxmjZyIEm_TZEMxYCOyZxCEHeS-s9g6Zo99k6qv0X372R4FGdm1GE9il5ogo6ZCPoIwYcvA0eaEjkjvPBe9WtTXEbHntoNF9X_tbSAR0z_XsQIbk7qjNAB21y3W4=s0-d-e1-ft#https://img.fkcdn.com/image/xif0q/jean/k/z/v/36-mdnact2bc281darkblue-spykar-original-imags8j6r5hzqejd.jpeg"" alt=""Spykar Boot-Leg Men Dark Blue Jeans"" style=""border:none;max-width:125px;max-height:125px"">
{item.Quantity}</td>
                        </tr>"));

            return $@"
        <html>
        <head>
            <style>
                body {{ font-family: Arial; color: #333; }}
                h2, h3 {{ color: #0073e6; }}
                table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
                th, td {{ padding: 10px; border: 1px solid #ddd; text-align: left; }}
                th {{ background-color: #f5f5f5; }}
                .footer {{ margin-top: 30px; font-size: 0.9em; color: #666; }}
            </style>
        </head>
        <body>
            <h2>New Order Received</h2>
            <p>Order #: <strong>{order.OrderNumber}</strong></p>

            <h3>Customer Details:</h3>
            <p><strong>Name:</strong> {address.RecipientName}</p>
            <p><strong>Phone:</strong> {address.ContactNumber}</p>
            <p><strong>Address:</strong> {address.AddressLine}, {address.City}, {address.State} - {address.PostalCode}</p>

            <h3>Ordered Items:</h3>
            <table>
                <thead><tr><th>Product</th><th>Qty</th></tr></thead>
                <tbody>{itemsHtml}</tbody>
            </table>

            <p class='footer'>Please prepare the items for dispatch.</p>
        </body>
        </html>";
        }
    }
}