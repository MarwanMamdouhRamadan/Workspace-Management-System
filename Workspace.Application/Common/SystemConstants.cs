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
            public const string Invoice = "Invoice";
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
        public static class Rooms
        {
            public const string Active = "Active";
            public const string Closed = "Closed";
        }
        public static class RoomType
        {
            public const string Shared = "Shared";
            public const string Private = "Private";
            public const string Private_Group = "Private_Group";
        }
        public static class Booking
        {
            public const string Pending = "Pending";
            public const string Confirmed = "Confirmed";
            public const string Complete = "Complete";
            public const string Cancel = "Cancel";
        }
        public static class InvoiceStatus
        {
            public const string Pending = "Pending";
            public const string Paid = "Paid";
            public const string Cancelled = "Cancelled";
        }
    }
}
