using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestFashionShop
{
    public static class SessionInfo
    {
        public static int? IdUser { get; set; }
        public static string RoleUser { get; set; }
        public static string UserFullName { get; set; }
        public static string Passwords { get; set; }
    }
}