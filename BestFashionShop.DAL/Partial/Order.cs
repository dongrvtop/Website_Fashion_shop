using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFashionShop.DAL
{
    public partial class Order
    {
    }
    public class OrderDTO
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int UserId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPhone { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public Nullable<decimal> PaymentPrice { get; set; }
        public string StrPaymentPrice
        {
            get
            {
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US" 
                var result = "Đang cập nhật";
                if (PaymentPrice != null)
                {
                    result = double.Parse(PaymentPrice.ToString()).ToString("#,###", cul.NumberFormat) + "đ";
                    //result = string.Format("{0:#.000 đ}", Price / 1000);
                }
                return result;
            }
        }
        public List<OrderDetailDTO> LstOrderDetail { get; set; }
        public List<int> LstCartId { get; set; }
    }
}
