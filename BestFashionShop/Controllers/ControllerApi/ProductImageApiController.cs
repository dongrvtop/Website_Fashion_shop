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
    public class ProductImageApiController : ApiController
    {
        private BestFashionShopEntities db = new BestFashionShopEntities();
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public async Task<string> GetAllProductImage()
        {
            try
            {
                var result = db.ProductImages.Select(s => new ProductImageDTO
                {
                    Id = s.Id,
                    Url = s.Url,
                    DefaultImage = s.DefaultImage,
                    ProductId = s.ProductId
                }).ToList<ProductImageDTO>();
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public async Task<string> GetAllProductImageByProductId(int ProductId)
        {
            try
            {
                var result = db.ProductImages.Where(x => x.ProductId == ProductId).Select(s => new ProductImageDTO
                {
                    Id = s.Id,
                    Url = s.Url,
                    DefaultImage = s.DefaultImage,
                    ProductId = s.ProductId
                }).ToList<ProductImageDTO>();
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
        public async Task<int> AddProductImage(ProductImageDTO productImage)
        {
            try
            {
                var newImg = new ProductImage();
                newImg.ProductId = productImage.ProductId;
                newImg.Url = productImage.Url;
                newImg.DefaultImage = productImage.DefaultImage;
                db.ProductImages.Add(newImg);
                await db.SaveChangesAsync();
                return newImg.Id;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpPut]
        [AcceptVerbs("PUT","POST")]
        public async Task<int> UpdateProductImage(ProductImageDTO productImage)
        {
            try
            {
                var Img = db.ProductImages.FirstOrDefault(s => s.Id == productImage.Id);
                if(Img != null)
                {
                    Img.ProductId = productImage.ProductId;
                    Img.Url = productImage.Url;
                    Img.DefaultImage = productImage.DefaultImage;
                    Img.ProductId = productImage.ProductId;
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
        [HttpDelete]
        [AcceptVerbs("DELETE","POST")]
        public async Task<int> DeleteProductImageById(int Id)
        {
            try
            {
                var Img = db.ProductImages.FirstOrDefault(s => s.Id == Id);
                if(Img != null)
                {
                    db.ProductImages.Remove(Img);
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
        [AcceptVerbs("DELETE","POST")]
        public async Task<int> DeleteProductImageByProductId(int ProductId)
        {
            try
            {
                var lstImg = db.ProductImages.Where(s => s.ProductId == ProductId);
                if (lstImg.Count() > 0)
                {
                    foreach(var item in lstImg)
                    {
                        db.ProductImages.Remove(item);
                    }
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
    }
}
