using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        bool DeleteOrder(string c);
        Order GetByCode(string codiceOrdine);

        bool UpdateOrder(Order a, Order b);
    }
}
