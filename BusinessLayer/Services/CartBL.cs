using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        public readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public string AddBookToCart(CartModel cartModel, int userId)
        {
            try
            {
                return cartRL.AddBookToCart(cartModel, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string DeleteCart(int cartId)
        {
            try
            {
                return cartRL.DeleteCart(cartId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateCart(int cartId, int booksQty)
        {
            try
            {
                return cartRL.UpdateCart(cartId, booksQty);
            }catch(Exception e)
            {
                throw e;
            }
        }
        public List<CartModel> GetAllBooksinCart(int userId)
        {
            try
            {
                return cartRL.GetAllBooksinCart(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
