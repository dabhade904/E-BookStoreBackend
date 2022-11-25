using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackModel
    {
        public string Comment { get; set; }
        public float Rating { get; set; }
        public int BookId { get; set; }
    }
}
