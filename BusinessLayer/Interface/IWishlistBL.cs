using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public string AddBookinWishList(WishlistModel wishListModel, int userId);
        public List<WishlistModel> GetAllBooksinWishList(int userId);
        public string DeleteFromWishList(int wishListId);

    }
}
