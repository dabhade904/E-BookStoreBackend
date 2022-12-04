using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public string AddBookToCart(CartModel cartModel, int userId, int bookId);
        public string DeleteCart(int cartId);
        public bool UpdateCart(int cartId, int booksQty);
        public List<CartResponce> GetAllBooksinCart(int userId);

    }
}
