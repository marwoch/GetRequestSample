using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetRequestSample.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}