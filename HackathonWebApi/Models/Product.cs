using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int Count { get; set; }

        public DateTime Date { get; set; }
        public String DateString { get; set; }

        public int OrderId { get; set; }
    }
}