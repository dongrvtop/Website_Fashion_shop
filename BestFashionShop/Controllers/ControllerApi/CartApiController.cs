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
        [AcceptVerbs("GET")]
        public async Task<string> GetAllCarts()
        {
            try
            {
                var result = db.Carts.Select(s => new CartDTO
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    UserId = s.UserId,
                    Quantity = s.Quantity,
                    Price = s.Price
                }).ToList<CartDTO>();
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
        public async Task<int> AddCarts(CartDTO cart)
        {
            try
            {
                var itemCart = db.Carts.FirstOrDefault(s => s.ProductId == cart.ProductId);
                if(itemCart != null)
                {
                    itemCart.Quantity = cart.Quantity;
                }
                else
                {
                    var newItem = new Cart();
                    newItem.ProductId = cart.ProductId;
                    newItem.UserId = cart.UserId;
                    newItem.Quantity = cart.Quantity;
                    newItem.Price = cart.Price;
                    db.Carts.Add(newItem);
                }
                await db.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }
        }
        [HttpPut]
        [AcceptVerbs("DELETE","POST")]
        public async Task<int> DeleteCartsByProductId(int ProductId)
        {
            try
            {
                var itemCart = db.Carts.FirstOrDefault(s => s.ProductId == ProductId);
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
                throw;
            }
        }
    }
}
