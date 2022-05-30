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
    public class OrderApiController : ApiController
    {
        private BestFashionShopEntities db = new DAL.BestFashionShopEntities();
        [HttpPost]
        [AcceptVerbs("POST", "PUT")]
        public async Task<int> AddOrders(OrderDTO order)
        {
            try
            {
                var newOrder = new Order();
                newOrder.ShipName = order.ShipName;
                newOrder.UserId = order.UserId;
                newOrder.ShipPhone = order.ShipPhone;
                newOrder.ShipAddress = order.ShipAddress;
                newOrder.Status = 1;
                newOrder.OrderDate = DateTime.Now;
                newOrder.PaymentPrice = order.PaymentPrice;
                db.Orders.Add(newOrder);
                await db.SaveChangesAsync();
                foreach(var ord in order.LstOrderDetail)
                {
                    var newOrdDetail = new OrderDetail();
                    newOrdDetail.OrderId = newOrder.Id;
                    newOrdDetail.ProductId = ord.ProductId;
                    newOrdDetail.ProductQuantity = ord.ProductQuantity;
                    var prod = db.Products.FirstOrDefault(x => x.Id == ord.ProductId);
                    prod.Quantity -= ord.ProductQuantity;
                    if(prod.Quantity <= 0)
                    {
                        return 2;
                    }
                    db.OrderDetails.Add(newOrdDetail);
                    db.SaveChanges();
                }
                //db.SaveChanges();
                List<Cart> lstCartRemove = new List<Cart>();
                foreach (var cart in order.LstCartId)
                {
                    var itCart = db.Carts.FirstOrDefault(x => x.Id == cart);
                    lstCartRemove.Add(itCart);
                }
                db.Carts.RemoveRange(lstCartRemove);
                db.SaveChanges();
                return 1;            
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [HttpPost]
        [AcceptVerbs("POST", "PUT")]
        public async Task<int> UpdateOrders(int Id, int Status)
        {
            try
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == Id);
                if(order != null)
                {
                    if(order.Status > Status)
                    {
                        return 0;
                    }
                    order.Status = Status;
                    await db.SaveChangesAsync();
                    if (Status == 4)
                    {
                        var lstOrder = db.OrderDetails.Where(x => x.OrderId == Id).ToList();
                        foreach(var ordDetail in lstOrder)
                        {
                            var product = db.Products.FirstOrDefault(x => x.Id == ordDetail.ProductId);
                            product.Quantity += ordDetail.ProductQuantity;
                            db.SaveChanges();
                        }
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
        [HttpGet]
        [AcceptVerbs("POST", "GET")]
        public string GetAllOrder(int? pageSize, int? currentPage)
        {
            try
            {
                var lstOrder = db.Orders.Select(x => new OrderDTO
                {
                    Id = x.Id,
                    ShipName = x.ShipName,
                    ShipPhone = x.ShipPhone,
                    ShipAddress = x.ShipAddress,
                    Status = x.Status,
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    PaymentType = x.PaymentType,
                    PaymentPrice = x.PaymentPrice
                }).OrderByDescending(x=>x.Id).Skip((int)(pageSize * (currentPage - 1))).Take((int)pageSize).ToList();
                foreach (var order in lstOrder)
                {
                    order.LstOrderDetail = db.OrderDetails.Where(x => x.OrderId == order.Id).Select(p => new OrderDetailDTO
                    {
                        Id = p.Id,
                        OrderId = p.OrderId,
                        ProductId = p.ProductId,
                        ProductQuantity = p.ProductQuantity
                    }).ToList();
                }
                var res = new Common.TypeResult.ListResult();
                res.data = lstOrder;
                res.totalRowCount = db.Orders.Count();
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
        [AcceptVerbs("POST", "GET")]
        public string GetMyOrder(int? pageSize, int? currentPage, int UserId)
        {
            try
            {
                var lstOrder = db.Orders.Where(x=>x.UserId == UserId).Select(x => new OrderDTO
                {
                    Id = x.Id,
                    ShipName = x.ShipName,
                    ShipPhone = x.ShipPhone,
                    ShipAddress = x.ShipAddress,
                    Status = x.Status,
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    PaymentType = x.PaymentType,
                    PaymentPrice = x.PaymentPrice
                }).OrderByDescending(x => x.Id).Skip((int)(pageSize * (currentPage - 1))).Take((int)pageSize).ToList();
                foreach (var order in lstOrder)
                {
                    order.LstOrderDetail = db.OrderDetails.Where(x => x.OrderId == order.Id).Select(p => new OrderDetailDTO
                    {
                        Id = p.Id,
                        OrderId = p.OrderId,
                        ProductId = p.ProductId,
                        ProductQuantity = p.ProductQuantity
                    }).ToList();
                }
                var res = new Common.TypeResult.ListResult();
                res.data = lstOrder;
                res.totalRowCount = db.Orders.Count();
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
        [AcceptVerbs("POST", "GET")]
        public string GetDetailOrder(int OrderId)
        {
            try
            {
                var order = db.Orders.Where(x => x.Id == OrderId).Select(x => new OrderDTO
                {
                    Id = x.Id,
                    ShipName = x.ShipName,
                    ShipPhone = x.ShipPhone,
                    ShipAddress = x.ShipAddress,
                    Status = x.Status,
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    PaymentType = x.PaymentType,
                    PaymentPrice = x.PaymentPrice
                }).FirstOrDefault();
                order.LstOrderDetail = db.OrderDetails.Where(x => x.OrderId == order.Id).Select(p => new OrderDetailDTO
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    ProductId = p.ProductId,
                    ProductQuantity = p.ProductQuantity
                }).ToList();
                //var res = new Common.TypeResult.ListResult();
                //res.data = order;
                return JsonConvert.SerializeObject(order);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
    }
}
