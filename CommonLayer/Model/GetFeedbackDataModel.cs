using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetFeedbackDataModel
    {
        public int FeedbackId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }

    }
}
