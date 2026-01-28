using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Application.Common
{
    public static class SystemConstants
    {
        public static class StatusTypes
        {
            public const string Product = "Product";
            public const string Room = "Room";
            public const string Booking = "Booking";
        }
        public static class ProductStatus
        {
            public const string Active = "Active";
            public const string Closed = "Closed";
        }
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string Customer = "Customer";
        }
    }
}
