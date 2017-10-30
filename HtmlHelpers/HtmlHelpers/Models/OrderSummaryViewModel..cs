using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HtmlHelpers.Models
{
    public class OrderSummaryViewModel
    {
        public int OrderId { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}