using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public string AddBookinWishList(WishlistModel wishListModel, int userId);
        public List<WishlistModel> GetAllBooksinWishList(int userId);

    }
}
