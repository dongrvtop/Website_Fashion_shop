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
    public class MyCollectionApiController : ApiController
    {
        private BestFashionShopEntities db = new BestFashionShopEntities();
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public async Task<List<MyCollectionDTO>> GetAllMyCollection()
        {
            try
            {
                var lstItem = db.MyCollections.Select(s => new MyCollectionDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Image = s.Image,
                    MetaTitle = s.MetaTitle,
                    SeoTitle = s.SeoTitle,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    Status = s.Status
                }).ToList<MyCollectionDTO>();
                return lstItem;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpGet]
        [AcceptVerbs("GET","POST")]
        public async Task<MyCollectionDTO> GetMyCollectionById(int Id)
        {
            try
            {
                var lstItem = db.MyCollections.Where(x=>x.Id == Id).Select(s => new MyCollectionDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Image = s.Image,
                    MetaTitle = s.MetaTitle,
                    SeoTitle = s.SeoTitle,
                    CreateDate = s.CreateDate,
                    CreateBy = s.CreateBy,
                    ModifiedDate = s.ModifiedDate,
                    ModifiedBy = s.ModifiedBy,
                    Status = s.Status
                });
                return (MyCollectionDTO)lstItem;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [HttpPost]
        [AcceptVerbs("POST")]
        public async Task<int> AddMyCollection(MyCollectionDTO myCollection)
        {
            try
            {
                MyCollection item = new MyCollection();
                item.Name = myCollection.Name;
                item.Description = myCollection.Description;
                item.Image = myCollection.Image;
                item.MetaTitle = myCollection.MetaTitle;
                item.SeoTitle = myCollection.SeoTitle;
                item.CreateDate = myCollection.CreateDate;
                item.CreateBy = myCollection.CreateBy;
                item.ModifiedDate = myCollection.ModifiedDate;
                item.ModifiedBy = myCollection.ModifiedBy;
                item.Status = myCollection.Status;
                db.MyCollections.Add(item);
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
        public async Task<int> UpdateMyCollection(MyCollectionDTO myCollection)
        {
            try
            {
                var item = db.MyCollections.FirstOrDefault(s => s.Id == myCollection.Id);
                if (item != null)
                {
                    item.Name = myCollection.Name;
                    item.Description = myCollection.Description;
                    item.Image = myCollection.Image;
                    item.MetaTitle = myCollection.MetaTitle;
                    item.SeoTitle = myCollection.SeoTitle;
                    item.CreateDate = myCollection.CreateDate;
                    item.CreateBy = myCollection.CreateBy;
                    item.ModifiedDate = myCollection.ModifiedDate;
                    item.ModifiedBy = myCollection.ModifiedBy;
                    item.Status = myCollection.Status;
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
        public async Task<int> DeleteMyCollection(int Id)
        {
            try
            {
                var item = db.MyCollections.FirstOrDefault(s => s.Id == Id);
                if (item != null)
                {
                    var lstProduct = db.Products.Where(s => s.CollectionId == Id);
                    if (lstProduct.Count() > 0)
                    {
                        foreach(var product in lstProduct)
                        {
                            db.Products.Remove(product);
                        }
                    }
                    db.MyCollections.Remove(item);
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
