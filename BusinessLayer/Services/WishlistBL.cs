using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL:IWishlistBL
    {
        public readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public string AddBookinWishList(WishlistModel wishListModel, int userId)
        {
            try
            {
                return wishlistRL.AddBookinWishList(wishListModel, userId); 
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<WishlistModel> GetAllBooksinWishList(int userId)
        {
            try
            {
                return wishlistRL.GetAllBooksinWishList(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DeleteFromWishList(int wishListId)
        {
            try
            {
                return wishlistRL.DeleteFromWishList(wishListId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
