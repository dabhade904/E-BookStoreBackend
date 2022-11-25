using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public string AddOrder(OrderModel order);
        public List<GetOrderDetailsModel> AllOrderDetails(int userId);

    }
}
