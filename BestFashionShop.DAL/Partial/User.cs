using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFashionShop.DAL
{
    public partial class User
    {
    }
    public class UserDTO
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Name { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public string StrRole
        {
            get
            {
                BestFashionShopEntities db = new BestFashionShopEntities();
                var role = db.Roles.FirstOrDefault(x => x.Id == RoleId);
                if (role != null)
                {
                    return role.Name;
                }
                return "";
            }
        }
    }
}
