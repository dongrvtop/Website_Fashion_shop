using BestFashionShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BestFashionShop.Controllers.ControllerApi
{
    public class ProductCategoryApiController : ApiController
    {
        private BestFashionShopEntities db = new BestFashionShopEntities();
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public async Task<List<ProductCategoryDTO>> GetAllProductCategory()
        {
            try
            {
                var listItem = db.ProductCategories.Select(s => new ProductCategoryDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    MetaTitle = s.MetaTitle,
                    ParentId = s.ParentId,
                    DisplayOrder = s.DisplayOrder,
                    SeoTitle = s.SeoTitle,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    MetaKeywords = s.MetaKeywords,
                    Description = s.Description,
                    Status = s.Status,
                    ShowOnHome = s.ShowOnHome,
                    CategoryId = s.CategoryId
                }).ToList<ProductCategoryDTO>();
                return listItem;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public async Task<ProductCategoryDTO> GetProductCategoryById(int Id)
        {
            try
            {
                var item = db.ProductCategories.Where(s=>s.Id == Id).Select(s => new ProductCategoryDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    MetaTitle = s.MetaTitle,
                    ParentId = s.ParentId,
                    DisplayOrder = s.DisplayOrder,
                    SeoTitle = s.SeoTitle,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    MetaKeywords = s.MetaKeywords,
                    Description = s.Description,
                    Status = s.Status,
                    ShowOnHome = s.ShowOnHome,
                    CategoryId = s.CategoryId
                });
                return (ProductCategoryDTO)item;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpPost]
        [AcceptVerbs("GET","POST")]
        public async Task<int> AddProductCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                ProductCategory newItem = new ProductCategory();
                newItem.Id = productCategory.Id;
                newItem.Name = productCategory.Name;
                newItem.MetaTitle = productCategory.MetaTitle;
                newItem.ParentId = productCategory.ParentId;
                newItem.DisplayOrder = productCategory.DisplayOrder;
                newItem.SeoTitle = productCategory.SeoTitle;
                newItem.CreateDate = DateTime.Now;
                newItem.CreateBy = productCategory.CreateBy;
                newItem.MetaKeywords = productCategory.MetaKeywords;
                newItem.Description = productCategory.Description;
                newItem.Status = productCategory.Status;
                newItem.ShowOnHome = productCategory.ShowOnHome;
                newItem.CategoryId = productCategory.CategoryId;
                db.ProductCategories.Add(newItem);
                await db.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpPut]
        [AcceptVerbs("PUT","POST")]
        public async Task<int> UpdateProductCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                ProductCategory newItem = new ProductCategory();
                var item = db.ProductCategories.FirstOrDefault(s => s.Id == productCategory.Id);
                if(item != null)
                {
                    item.Id = productCategory.Id;
                    item.Name = productCategory.Name;
                    item.MetaTitle = productCategory.MetaTitle;
                    item.ParentId = productCategory.ParentId;
                    item.DisplayOrder = productCategory.DisplayOrder;
                    item.SeoTitle = productCategory.SeoTitle;
                    item.ModifiedDate = productCategory.ModifiedDate;
                    item.ModifiedBy = productCategory.ModifiedBy;
                    item.MetaKeywords = productCategory.MetaKeywords;
                    item.Description = productCategory.Description;
                    item.Status = productCategory.Status;
                    item.ShowOnHome = productCategory.ShowOnHome;
                    item.CategoryId = productCategory.CategoryId;
                    await db.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpDelete]
        [AcceptVerbs("DELETE","POST")]
        public async Task<int> DeleteProductCategory(int Id)
        {
            try
            {
                ProductCategory newItem = new ProductCategory();
                var item = db.ProductCategories.FirstOrDefault(s => s.Id == Id);
                if(item != null)
                {
                    db.ProductCategories.Remove(item);
                    await db.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
    }
}
