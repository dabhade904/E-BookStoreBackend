using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public string AddToWishList(int bookId, int userId);
        public List<WishlistModel> GetAllWishList(int userId);
        public string RemoveFromWishList(int wishListId);



    }
}
