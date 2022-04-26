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
    public class ProductApiController : ApiController
    {
        private BestFashionShopEntities db = new DAL.BestFashionShopEntities();
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            try
            {
                var result = db.Products.Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    MetaTitle = s.MetaTitle,
                    Description = s.Description,
                    Price = s.Price,
                    PromotionPrice = s.PromotionPrice,
                    Quantity = s.Quantity,
                    CategoryId = s.CategoryId,
                    Detail = s.Detail,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                    Status = s.Status,
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                }).ToList<ProductDTO>();
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
        public async Task<List<ProductDTO>> GetListProductByProductCategoryId(int Id)
        {
            try
            {
                var result = db.Products.Where(s=>s.CategoryId == Id).Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    MetaTitle = s.MetaTitle,
                    Description = s.Description,
                    Price = s.Price,
                    PromotionPrice = s.PromotionPrice,
                    Quantity = s.Quantity,
                    CategoryId = s.CategoryId,
                    Detail = s.Detail,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                    Status = s.Status,
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                }).ToList<ProductDTO>();
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
        public async Task<ProductDTO> GetProductById(int Id)
        {
            try
            {
                var result = db.Products.Where(s => s.Id == Id).Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    MetaTitle = s.MetaTitle,
                    Description = s.Description,
                    Price = s.Price,
                    PromotionPrice = s.PromotionPrice,
                    Quantity = s.Quantity,
                    CategoryId = s.CategoryId,
                    Detail = s.Detail,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = s.ModifiedDate,
                    Status = s.Status,
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpPost]
        [AcceptVerbs("POST")]
        public async Task<int> AddProduct(ProductDTO product)
        {
            try
            {
                var item = new Product();
                item.Name = product.Name;
                item.Code = product.Code;
                item.MetaTitle = product.MetaTitle;
                item.Description = product.Description;
                item.Price = product.Price;
                item.PromotionPrice = product.PromotionPrice;
                item.Quantity = product.Quantity;
                item.CategoryId = product.CategoryId;
                item.Detail = product.Detail;
                item.CreateDate = DateTime.Now;
                item.CreateBy = null;
                item.Status = product.Status;
                item.TopHot = product.TopHot;
                item.CollectionId = product.CollectionId;
                item.NewProduct = product.NewProduct;
                db.Products.Add(item);
                await db.SaveChangesAsync();
                var result = item.Id;
                return result;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpDelete]
        [AcceptVerbs("DELETE", "POST")]
        public async Task<int> RemoveProductById(int Id)
        {
            try
            {
                var item = db.Products.FirstOrDefault(s => s.Id == Id);
                if(item != null)
                {
                    var itemImage = db.ProductImages.Where(s => s.ProductId == Id);
                    if (itemImage.Count() > 0)
                    {
                        db.ProductImages.RemoveRange(itemImage);
                        
                    }
                    db.Products.Remove(item);
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpDelete]
        [AcceptVerbs("DELETE", "POST")]
        public async Task<int> RemoveProductByProductCategoryId(int Id)
        {
            try
            {
                var listItem = db.Products.Where(s => s.CategoryId == Id).ToList<Product>();
                if(listItem.Count() > 0)
                {
                    foreach (var item in listItem)
                    {
                        var product = db.Products.FirstOrDefault(s => s.Id == item.Id);
                        if (product != null)
                        {
                            var itemImage = db.ProductImages.Where(s => s.ProductId == product.Id).ToList<ProductImage>();
                            if (itemImage.Count() > 0)
                            {
                                db.ProductImages.RemoveRange(itemImage);

                            }
                            db.Products.Remove(product);
                        }
                    }
                    await db.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpDelete]
        [AcceptVerbs("DELETE", "POST")]
        public async Task<int> RemoveListProductByListId(List<int> lstId)
        {
            try
            {
                foreach(var item in lstId)
                {
                    var product = db.Products.FirstOrDefault(s => s.Id == item);
                    if (product != null)
                    {
                        var itemImage = db.ProductImages.Where(s => s.ProductId == item);
                        if (itemImage.Count() > 0)
                        {
                            db.ProductImages.RemoveRange(itemImage);

                        }
                        db.Products.Remove(product);
                    }
                }
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
        public async Task<int> UpdateProduct(ProductDTO product)
        {
            try
            {
                var item = db.Products.FirstOrDefault(s => s.Id == product.Id);
                if(item != null)
                {
                    item.Name = product.Name;
                    item.Code = product.Code;
                    item.MetaTitle = product.MetaTitle;
                    item.Description = product.Description;
                    item.Price = product.Price;
                    item.PromotionPrice = product.PromotionPrice;
                    item.Quantity = product.Quantity;
                    item.CategoryId = product.CategoryId;
                    item.Detail = product.Detail;
                    item.ModifiedDate = DateTime.Now;
                    item.ModifiedBy = null;
                    item.Status = product.Status;
                    item.TopHot = product.TopHot;
                    item.CollectionId = product.CollectionId;
                    item.NewProduct = product.NewProduct;
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }
        }

    }
}
