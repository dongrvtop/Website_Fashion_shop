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
    public class CategoryApiController : ApiController
    {
        private BestFashionShopEntities db = new DAL.BestFashionShopEntities();
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            try
            {
                var result = db.Categories.Select(s => new CategoryDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    MetaTitle = s.MetaTitle,
                    SeoTitle = s.SeoTitle,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    MetaKeywords = s.MetaKeywords,
                    Description = s.Description
                }).ToList<CategoryDTO>();
                return result;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public async Task<string> GetCategoryById(int Id)
        {
            try
            {
                var result = db.Categories.Where(s=>s.Id == Id).Select(s => new CategoryDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    MetaTitle = s.MetaTitle,
                    SeoTitle = s.SeoTitle,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    MetaKeywords = s.MetaKeywords,
                    Description = s.Description
                }).FirstOrDefault<CategoryDTO>();
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpPost]
        [AcceptVerbs("POST")]
        public async Task<int> AddCategory(CategoryDTO category)
        {
            try
            {
                var item = new Category();
                item.Name = category.Name;
                item.MetaTitle = category.MetaTitle;
                item.SeoTitle = category.SeoTitle;
                item.CreateDate = DateTime.Now;
                item.CreateBy = category.CreateBy;
                item.MetaKeywords = category.MetaKeywords;
                item.Description = category.Description;
                db.Categories.Add(item);
                await db.SaveChangesAsync();
                return item.Id;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        [HttpPut]
        [AcceptVerbs("PUT","POST")]
        public async Task<int> UpdateCategory(CategoryDTO category)
        {
            try
            {
                var item = db.Categories.FirstOrDefault(s => s.Id == category.Id);
                if(item != null)
                {
                    item.Name = category.Name;
                    item.MetaTitle = category.MetaTitle;
                    item.SeoTitle = category.SeoTitle;
                    item.ModifiedDate = DateTime.Now;
                    item.ModifiedBy = category.ModifiedBy;
                    item.MetaKeywords = category.MetaKeywords;
                    item.Description = category.Description;
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
        public async Task<int> DeleteCategory(int Id)
        {
            try
            {
                var item = db.Categories.FirstOrDefault(s => s.Id == Id);
                if (item != null)
                {
                    var lstProductCategory = db.ProductCategories.Where(x => x.CategoryId == Id);
                    if(lstProductCategory.Count() > 0)
                    {
                        foreach(var productCategory in lstProductCategory)
                        {
                            db.ProductCategories.Remove(productCategory);
                        }
                    }
                    db.Categories.Remove(item);
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
