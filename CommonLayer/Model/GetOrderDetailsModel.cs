using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetOrderDetailsModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public BookModel model { get; set; }
    }
}
