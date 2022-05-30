using BestFashionShop.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<string> GetAllProduct(int? pageSize,int? currentPage )
        {
            ProductImageApiController flg = new ProductImageApiController();
            try
            {
                var result = db.Products.Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
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
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                    PromotionPercent = s.PromotionPercent
                }).OrderBy(x => x.Id).Skip((int)(pageSize * (currentPage-1))).Take((int)pageSize).ToList();
                foreach (var item in result)
                {
                    item.ProductImages = db.ProductImages.Where(x => x.ProductId == item.Id).Select(p => new ProductImageDTO
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        DefaultImage = p.DefaultImage,
                        Url = p.Url
                    }).ToList<ProductImageDTO>();
                    // item.ProductImages = await flg.GetAllProductImageByProductId(item.Id);
                }

                var res = new Common.TypeResult.ListResult();
                res.data = result;
                res.totalRowCount = db.Products.Count();
                res.pageCount = (int)(res.totalRowCount / pageSize) + 1;
                res.pageStart = (pageSize * (currentPage - 1) + 1).GetValueOrDefault();
                res.pageEnd = (res.pageStart + pageSize - 1).GetValueOrDefault() <= res.totalRowCount ? (res.pageStart + pageSize - 1).GetValueOrDefault() : res.totalRowCount;
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public string GetListProductByProductCategoryId(int Id, int?pageSize, int currentPage)
        {
            try
            {
                var result = db.Products.Where(s => s.CategoryId == Id).Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
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
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                    PromotionPercent = s.PromotionPercent
                }).OrderBy(x => x.Id).Skip((int)(pageSize * (currentPage - 1))).Take((int)pageSize).ToList();
                foreach (var item in result)
                {
                    item.ProductImages = db.ProductImages.Where(x => x.ProductId == item.Id).Select(p => new ProductImageDTO
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        DefaultImage = p.DefaultImage,
                        Url = p.Url
                    }).ToList();
                }
                var res = new Common.TypeResult.ListResult();
                res.data = result;
                res.totalRowCount = db.Products.Where(s => s.CategoryId == Id).Count();
                res.pageCount = (int)(res.totalRowCount / pageSize) + 1;
                res.pageStart = (pageSize * (currentPage - 1) + 1).GetValueOrDefault();
                res.pageEnd = (res.pageStart + pageSize - 1).GetValueOrDefault() <= res.totalRowCount ? (res.pageStart + pageSize - 1).GetValueOrDefault() : res.totalRowCount;
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public string GetListProductByCollectionId(int Id, int? pageSize, int? currentPage)
        {
            try
            {
                var result = db.Products.Where(s => s.CollectionId == Id).Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
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
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                    PromotionPercent = s.PromotionPercent
                }).OrderBy(x => x.Id).Skip((int)(pageSize * (currentPage - 1))).Take((int)pageSize).ToList();
                foreach (var item in result)
                {
                    item.ProductImages = db.ProductImages.Where(x => x.ProductId == item.Id).Select(p => new ProductImageDTO
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        DefaultImage = p.DefaultImage,
                        Url = p.Url
                    }).ToList();
                }
                var res = new Common.TypeResult.ListResult();
                res.data = result;
                res.totalRowCount = db.Products.Where(s => s.CollectionId == Id).Count();
                res.pageCount = (int)(res.totalRowCount / pageSize) + 1;
                res.pageStart = (pageSize * (currentPage - 1) + 1).GetValueOrDefault();
                res.pageEnd = (res.pageStart + pageSize - 1).GetValueOrDefault() <= res.totalRowCount ? (res.pageStart + pageSize - 1).GetValueOrDefault() : res.totalRowCount;
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public string SearchProduct(string value)
        {
            try
            {
                var result = db.Products.Where(s => s.Name.Contains(@"" + value + "")).Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                }).ToList<ProductDTO>();
                foreach (var item in result)
                {
                    item.ProductImages = db.ProductImages.Where(x => x.ProductId == item.Id && x.DefaultImage == true).Select(p => new ProductImageDTO
                    {
                        Id = p.Id,
                        DefaultImage = p.DefaultImage,
                        Url = p.Url
                    }).ToList();
                }
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET", "POST")]
        public async Task<string> GetProductById(int Id)
        {
            try
            {
                var result = db.Products.Where(s => s.Id == Id).Select(s => new ProductDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
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
                    TopHot = s.TopHot,
                    CollectionId = s.CollectionId,
                    NewProduct = s.NewProduct,
                    PromotionPercent = s.PromotionPercent
                }).FirstOrDefault();
                result.ProductImages = db.ProductImages.Where(x => x.ProductId == result.Id).Select(p => new ProductImageDTO
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    DefaultImage = p.DefaultImage,
                    Url = p.Url
                }).ToList();
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
        public async Task<int> AddProduct(ProductDTO product)
        {
            try
            {
                var prod = new Product();
                prod.Name = product.Name;
                prod.Code = product.Code;
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.PromotionPrice = product.PromotionPrice;
                prod.Quantity = product.Quantity;
                prod.CategoryId = product.CategoryId;
                prod.Detail = product.Detail;
                prod.CreateDate = DateTime.Now;
                prod.CreateBy = null;
                prod.TopHot = product.TopHot;
                prod.CollectionId = product.CollectionId;
                prod.NewProduct = product.NewProduct;
                prod.PromotionPercent = product.PromotionPercent;
                db.Products.Add(prod);
                await db.SaveChangesAsync();
                if (product.ProductImages != null)
                {
                    foreach(var item in product.ProductImages)
                    {
                        var newItem = new ProductImage();
                        newItem.Url = item.Url;
                        newItem.ProductId = prod.Id;
                        newItem.DefaultImage = item.DefaultImage;
                        db.ProductImages.Add(newItem);
                    }
                    await db.SaveChangesAsync();
                }
                var result = prod.Id;
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
                    var itemImage = db.ProductImages.Where(s => s.ProductId == Id).ToList();
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
                    item.Description = product.Description;
                    item.Price = product.Price;
                    item.PromotionPrice = product.PromotionPrice;
                    item.Quantity = product.Quantity;
                    item.CategoryId = product.CategoryId;
                    item.Detail = product.Detail;
                    item.ModifiedDate = DateTime.Now;
                    item.ModifiedBy = null;
                    item.TopHot = product.TopHot;
                    item.CollectionId = product.CollectionId;
                    item.NewProduct = product.NewProduct;
                    item.PromotionPercent = product.PromotionPercent;
                    var lstProductImage = db.ProductImages.Where(x => x.ProductId == product.Id).ToList();
                    db.ProductImages.RemoveRange(lstProductImage);
                    await db.SaveChangesAsync();
                    if (product.ProductImages != null)
                    {
                        foreach (var prod in product.ProductImages)
                        {
                            var newItem = new ProductImage();
                            newItem.Url = prod.Url;
                            newItem.ProductId = product.Id;
                            newItem.DefaultImage = prod.DefaultImage;
                            db.ProductImages.Add(newItem);
                        }
                        await db.SaveChangesAsync();
                    }
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

    }
}
