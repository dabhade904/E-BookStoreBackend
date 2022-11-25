using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public string AddToWishList(int bookId, int userId);
        public List<WishlistModel> GetAllWishList(int userId);
        public string RemoveFromWishList(int wishListId);


    }
}
