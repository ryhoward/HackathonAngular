using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonWebApi.Models
{
    public class CustomerOrder
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string BillingStreet { get; set; }
        public string BillingStreet2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }

        public string ShippingStreet { get; set; }
        public string ShippingStreet2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }

        public int UserId { get; set; }

    }
}