using BestFashionShop.DAL;
using Newtonsoft.Json;
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
        public async Task<string> GetAllProductCategory()
        {
            try
            {
                var listItem = db.ProductCategories.Select(s => new ProductCategoryDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    Description = s.Description,
                    CategoryId = s.CategoryId
                }).ToList<ProductCategoryDTO>();
                return JsonConvert.SerializeObject(listItem);
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
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    Description = s.Description,
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
                newItem.CreateDate = DateTime.Now;
                newItem.CreateBy = productCategory.CreateBy;
                newItem.Description = productCategory.Description;
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
                    item.ModifiedDate = productCategory.ModifiedDate;
                    item.ModifiedBy = productCategory.ModifiedBy;
                    item.Description = productCategory.Description;
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
