using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class BookModel
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookImage { get; set; }
        public string BookDetail { get; set; }
        public double DiscountPrice { get; set; }
        public double ActualPrice { get; set; }
        public int Quantity { get; set; }
        public float Rating { get; set; }
        public float RatingCount { get; set; }
    }
}
