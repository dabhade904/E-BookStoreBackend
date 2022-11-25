using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL=orderRL;
        }
        public string AddOrder(OrderModel order)      
        {
            try
            {
                return orderRL.AddOrder(order);    
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
