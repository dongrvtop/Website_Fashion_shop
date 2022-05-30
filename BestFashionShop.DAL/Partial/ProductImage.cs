using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFashionShop.DAL
{
    public partial class ProductImage
    {

    }
    public class ProductImageDTO
    {
        public int? Id { get; set; }
        public string Url { get; set; }
        public Nullable<bool> DefaultImage { get; set; }
        public Nullable<int> ProductId { get; set; }
    }
}
