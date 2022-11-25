using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public string AddOrder(OrderModel order);
        public List<GetOrderDetailsModel> AllOrderDetails(int userId);

    }

}
