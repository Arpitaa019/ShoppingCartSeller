namespace ShoppingCart.Services.Service.Client
{
    internal class IMailGenerator
    {
        //@model OrderConfirmationDtTO


        //@"<!DOCTYPE html>
        //<html>
        //<head>
        //    <meta charset = "UTF-8" >
        //    < style >
        //        body {
        //            font - family: Arial, sans - serif; color: #333; }
        //        h2, h3 {
        //            color: #0073e6; }
        //        table { width: 100 %; border - collapse: collapse; margin - top: 20px; }
        //                th, td {
        //                padding: 10px; border: 1px solid #ddd; text-align: left; }
        //        th {
        //                        background - color: #f5f5f5; }
        //        img { max - width: 100px; }
        //        .footer {
        //                            margin - top: 30px; font - size: 0.9em; color: #666; }
        //    </ style >
        //</ head >
        //< body >
        //    < h2 > Hi @Model.CustomerName,</ h2 >
        //    < p > Your order<strong> @Model.OrderId </ strong > has been successfully placed on<strong>@Model.OrderDate.ToString("dd MMM yyyy")</ strong >.</ p >

        //    < h3 > Delivery Address:</ h3 >
        //    < p > @Model.Address </ p >

        //    < h3 > Order Summary:</ h3 >
        //    < table >
        //        < thead >
        //            < tr >
        //                < th > Image </ th >
        //                < th > Product </ th >
        //                < th > Seller </ th >
        //                < th > Qty </ th >
        //                < th > Price(INR) </ th >
        //                < th > Delivery Date </ th >
        //            </ tr >
        //        </ thead >
        //        < tbody >
        //        @foreach(var item in Model.Items)
        //        {
        //            < tr >
        //                < td >< img src = "@item.ImageUrl" alt = "@item.Title" /></ td >
        //                < td > @item.Title </ td >
        //                < td > @item.Seller </ td >
        //                < td > @item.Quantity </ td >
        //                < td >₹@item.Price.ToString("0.00") </ td >
        //                < td > @item.ExpectedDeliveryDate.ToString("dd MMM yyyy") </ td >
        //            </ tr >
        //        }
        //        </ tbody >
        //    </ table >

        //    < h3 > Payment Summary:</ h3 >
        //    < p >< strong > Total Paid:</ strong > ₹@Model.TotalAmountINR </ p >
        //    < p >< strong > SuperCoins Used:</ strong > @Model.SuperCoinsUsed </ p >
        //    < p >< strong > SuperCoins Earned:</ strong > @Model.SuperCoinsEarned </ p >

        //    < p class="footer">Thank you for shopping with us!<br/>Need help? Contact our 24x7 Customer Care.</p>
        //</body>
        //</html>"
    }
}
