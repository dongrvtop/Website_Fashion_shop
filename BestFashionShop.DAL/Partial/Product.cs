using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFashionShop.DAL
{
    public partial class Product
    {

    }
    public class ProductDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string StrPrice {
            get {
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US" 
                var result = "Đang cập nhật";
                if (Price != null)
                {
                    result = double.Parse(Price.ToString()).ToString("#,###", cul.NumberFormat) + "đ";
                    //result = string.Format("{0:#.000 đ}", Price / 1000);
                }
                return  result;
            }
        }
        public Nullable<decimal> PromotionPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName {
            get
            {
                BestFashionShopEntities db = new BestFashionShopEntities();
                if(CategoryId != null && CategoryId != 0)
                {
                    var result = db.ProductCategories.FirstOrDefault(x => x.Id == CategoryId).Name.ToString();
                    return result;
                }
                return null;
            }
        }
        public string Detail { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> TopHot { get; set; }
        public Nullable<int> CollectionId { get; set; }
        public bool? NewProduct { get; set; }
        public string PromotionPercent { get; set; }
        public List<ProductImageDTO> ProductImages { get; set; }
    }
    public class ProductImagesDTO
    {
        public int? Id { get; set; }
        public string Url { get; set; }
        public Nullable<bool> DefaultImage { get; set; }
        public int? ProductId { get; set; }
    }
}
