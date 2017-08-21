using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetRequestSample.Models
{
    public class SearchResults
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public double? MinimumPrice { get; set; }

        public double? MaximumPrice { get; set; }

        public List<Product> Products { get; set; }
    }
}