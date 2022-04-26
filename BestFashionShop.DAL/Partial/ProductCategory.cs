using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFashionShop.DAL
{
    public partial class ProductCategory
    {

    }
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<long> ParentId { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string SeoTitle { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> ShowOnHome { get; set; }
        public Nullable<int> CategoryId { get; set; }
    }
}
