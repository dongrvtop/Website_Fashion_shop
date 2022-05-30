using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFashionShop.DAL
{
    public partial class OrderDetail
    {
    }
    public class OrderDetailDTO
    {
        private BestFashionShopEntities db = new BestFashionShopEntities();
        public int Id { get; set; }
        public string ProductName {
            get
            {
                var prod = db.Products.FirstOrDefault(x => x.Id == ProductId);
                if(prod != null)
                {
                    return prod.Name;
                }
                return "";
            }
        }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int OrderId { get; set; }
        public List<ProductImageDTO> lstProductImages
        {
            get
            {
                var prod = db.Products.FirstOrDefault(x => x.Id == ProductId);
                if(prod != null)
                {
                    var result = db.ProductImages.Where(x => x.ProductId == prod.Id).Select(p => new ProductImageDTO
                    {
                        Id = p.Id,
                        Url = p.Url,
                        ProductId = p.ProductId,
                        DefaultImage = p.DefaultImage
                    }).ToList();
                    return result;
                }
                return null;
            }
        }
    }
}
