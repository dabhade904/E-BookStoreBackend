using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class CartResponce
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int CartsQty { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string Author { get; set; }
        public double DiscountPrice { get; set; }
        public double ActualPrice { get; set; }
        public int Stock { get; set; }
    }
}
