using BestFashionShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Common;
using BestFashionShop;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace BestFashionShop.Controllers.ControllerApi
{
    public class UserApiController : Controller
    {
        private BestFashionShopEntities db = new DAL.BestFashionShopEntities();
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public string Login(UserDTO user)
        {
            try
            {
                var result = new UserDTO();
                user.PasswordHash = Common.FunctionCommon.GetMD5(user.Password);
                var userLogin = db.Users.FirstOrDefault(x => (x.UserName == user.UserName || x.Email == user.Email && x.Email != null) && x.PasswordHash == user.PasswordHash);
                if(userLogin != null)
                {
                    //HttpCookie cookie = new HttpCookie("ADMIN");
                    //cookie["Username"] = userLogin.UserName;
                    //cookie.Expires = DateTime.Now.AddDays(30);
                    //Response.Cookies.Add(cookie);
                    //HttpCookie cookie = Request.Cookies["ADMIN"];
                    //cookie.Expires = DateTime.Now.AddDays(-1d);
                    //Response.Cookies.Add(cookie);
                    Session["IdUser"] = userLogin.Id;
                    Session["RoleUser"] = db.Roles.FirstOrDefault(x => x.Id == userLogin.RoleId).Name;
                    Session["UserFullName"] = userLogin.Name;
                    //SessionInfo.IdUser = userLogin.Id;
                    //SessionInfo.RoleUser = db.Roles.FirstOrDefault(x => x.Id == userLogin.RoleId).Name;
                    //SessionInfo.UserFullName = userLogin.Name;
                    result.Id = userLogin.Id;
                    result.Name = userLogin.Name;
                    result.Role = Session["RoleUser"].ToString();
                    return JsonConvert.SerializeObject(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public string AdminLogin(UserDTO user)
        {
            try
            {
                var result = new UserDTO();
                user.PasswordHash = Common.FunctionCommon.GetMD5(user.Password);
                var userLogin = db.Users.FirstOrDefault(x => (x.UserName == user.UserName || x.Email == user.Email && x.Email != null) && x.PasswordHash == user.PasswordHash);
                if (userLogin != null)
                {
                    //SessionInfo.IdUser = userLogin.Id;
                    //SessionInfo.RoleUser = db.Roles.FirstOrDefault(x => x.Id == userLogin.RoleId).Name;
                    //if (SessionInfo.RoleUser != "ADMIN")
                    //{
                    //    return null;
                    //}
                    //SessionInfo.UserFullName = userLogin.Name;
                    Session["IdUser"] = userLogin.Id;
                    Session["RoleUser"] = db.Roles.FirstOrDefault(x => x.Id == userLogin.RoleId).Name;
                    if (Session["RoleUser"].ToString() != "ADMIN")
                    {
                        return null;
                    }
                    Session["UserFullName"] = userLogin.Name;
                    result.Id = userLogin.Id;
                    result.Name = userLogin.Name;
                    result.Role = Session["RoleUser"].ToString();
                    return JsonConvert.SerializeObject(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public int Logout()
        {
            try
            {
                //SessionInfo.IdUser = null;
                //SessionInfo.RoleUser = null;
                //SessionInfo.UserFullName = null;
                Session.Clear();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public int Register(UserDTO user)
        {
            try
            {
                var userRegis = db.Users.FirstOrDefault(x => x.UserName == user.UserName);
                if(db.Users.FirstOrDefault(x => x.UserName == user.UserName) != null){
                    return 2;
                }
                if(db.Users.FirstOrDefault(x => x.Email == user.Email && user.Email != null) != null)
                {
                    return 3;
                }
                var newUser = new User();
                newUser.UserName = user.UserName;
                newUser.RoleId = 3;
                newUser.Phone = user.Phone;
                newUser.Name = user.Name;
                newUser.Birthday = user.Birthday;
                newUser.Email = user.Email;
                newUser.Password = user.Password;
                newUser.PasswordHash = Common.FunctionCommon.GetMD5(user.Password);
                db.Users.Add(newUser);
                db.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public string GetListUser(int? pageSize, int? currentPage)
        {
            try
            {
                var users = db.Users.Select(x=>new UserDTO
                {
                    Id = x.Id,
                    Name=x.Name,
                    Email=x.Email,
                    Phone=x.Phone,
                    Birthday=x.Birthday,
                    RoleId=x.RoleId
                }).OrderBy(x=>x.Id).Skip((int)(pageSize * (currentPage - 1))).Take((int)pageSize).ToList();
                var res = new Common.TypeResult.ListResult();
                res.data = users;
                res.totalRowCount = db.Users.Count();
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
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public string GetListRole()
        {
            try
            {
                var role = db.Roles.Select(x => new RoleDTO
                {
                    Id = x.Id,
                    Name = x.Name
                });
                return JsonConvert.SerializeObject(role);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public string GetUserById(int Id)
        {
            try
            {
                var user = db.Users.FirstOrDefault(x => x.Id == Id);
                if(user != null)
                {
                    var result = db.Users.Where(x => x.Id == Id).Select(x => new UserDTO
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        Email = x.Email,
                        Phone = x.Phone,
                        Birthday = x.Birthday,
                        Password = x.Password,
                        Name = x.Name,
                        RoleId = x.RoleId
                    }).FirstOrDefault();
                    return JsonConvert.SerializeObject(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST","PUT")]
        public int PostUserByAdmin(UserDTO user)
        {
            try
            {
                var userfg = db.Users.FirstOrDefault(x => x.Id == user.Id);
                if (userfg != null)
                {
                    userfg.Name = user.Name;
                    userfg.Email = user.Email;
                    userfg.Phone = user.Phone;
                    userfg.Birthday = user.Birthday;
                    userfg.RoleId = user.RoleId;
                    userfg.UserName = user.UserName;
                    userfg.Password = user.Password;
                    userfg.PasswordHash = Common.FunctionCommon.GetMD5(userfg.Password);
                    db.SaveChangesAsync();
                    return 2;
                }
                else
                {
                    var newUser = new User();
                    newUser.Name = user.Name;
                    newUser.Email = user.Email;
                    newUser.Phone = user.Phone;
                    newUser.Birthday = user.Birthday;
                    newUser.RoleId = user.RoleId;
                    newUser.UserName = user.UserName;
                    newUser.Password = user.Password;
                    newUser.PasswordHash = Common.FunctionCommon.GetMD5(userfg.Password);
                    db.Users.Add(newUser);
                    db.SaveChangesAsync();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
        [System.Web.Http.HttpDelete]
        [System.Web.Http.AcceptVerbs("POST", "DELETE")]
        public int RemoveUserByAdmin(int UserId)
        {
            try
            {
                var userfg = db.Users.FirstOrDefault(x => x.Id == UserId);
                if (userfg != null)
                {
                    if (userfg.RoleId == 1)
                    {
                        return 2;
                    }
                    db.Users.Remove(userfg);
                    db.SaveChangesAsync();
                    return 1;
                }
                return 3;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }
    }
}
