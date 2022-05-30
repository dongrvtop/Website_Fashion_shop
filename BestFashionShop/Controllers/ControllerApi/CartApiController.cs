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
    public class CartApiController : ApiController
    {
        private BestFashionShopEntities db = new BestFashionShopEntities();
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public string GetCartByUserId(int Id)
        {
            try
            {
                var result = db.Carts.Where(x => x.UserId == Id).Select(s => new CartDTO
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    UserId = s.UserId,
                    Quantity = s.Quantity,
                    Price = s.Price
                }).ToList<CartDTO>();
                foreach(var cart in result)
                {
                    var prod = db.Products.Where(x => x.Id == cart.ProductId).Select(s => new ProductDTO
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
                    prod.ProductImages = db.ProductImages.Where(x => x.ProductId == cart.ProductId && x.DefaultImage == true).Select(p => new ProductImageDTO
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        DefaultImage = p.DefaultImage,
                        Url = p.Url
                    }).ToList<ProductImageDTO>();
                    cart.Product = prod;
                }
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpPost]
        [AcceptVerbs("POST","PUT")]
        public async Task<int> AddOrUpdateCarts(CartDTO cart)
        {
            try
            {
                if(cart.Id != 0)
                {
                    var oldCart = db.Carts.FirstOrDefault(x => x.Id == cart.Id);
                    if (oldCart != null)
                    {
                        oldCart.ProductId = cart.ProductId;
                        oldCart.Quantity = cart.Quantity;
                        oldCart.Price = cart.Quantity * db.Products.FirstOrDefault(x => x.Id == cart.ProductId).Price;
                        await db.SaveChangesAsync();
                        return 1;
                    }
                }
                var old2Cart = db.Carts.FirstOrDefault(s => s.ProductId == cart.ProductId && s.UserId == cart.UserId);
                if (old2Cart != null)
                {
                    old2Cart.Quantity += cart.Quantity;
                    await db.SaveChangesAsync();
                    return 1;
                }
                
                //else
                //{
                var newItem = new Cart();
                    newItem.ProductId = cart.ProductId;
                    newItem.UserId = cart.UserId;
                    newItem.Quantity = cart.Quantity;
                    newItem.Price = cart.Price;
                    db.Carts.Add(newItem);
                //}
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
        [AcceptVerbs("DELETE","POST")]
        public async Task<int> DeleteCartsById(int Id)
        {
            try
            {
                var itemCart = db.Carts.FirstOrDefault(s => s.Id == Id);
                if(itemCart != null)
                {
                    db.Carts.Remove(itemCart);
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
