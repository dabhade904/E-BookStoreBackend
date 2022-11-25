using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        public readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public string AddToWishList(int bookId, int userId)
        {
            try
            {
                return this.wishlistRL.AddToWishList(bookId, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<WishlistModel> GetAllWishList(int userId)
        {
            try
            {
                return this.wishlistRL.GetAllWishList(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string RemoveFromWishList(int wishListId)
        {
            try
            {
                return this.wishlistRL.RemoveFromWishList(wishListId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
