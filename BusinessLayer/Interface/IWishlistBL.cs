using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
       public string AddBookinWishList(WishlistModel wishListModel, int userId);
    }
}
