using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
      public string AddBookToCart(CartModel cartModel, int userId);
        public string DeleteCart(int cartId);
        public bool UpdateCart(int cartId, int booksQty);


    }
}
