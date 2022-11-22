using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class WishlistModel
    {
        public int wishListId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public BookModel bookModel { get; set; }
    }
}
