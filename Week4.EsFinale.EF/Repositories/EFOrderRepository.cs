using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Week4.EsFinale.Core.Interfaces;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.EF.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly OrderContext ctx;

        public EFOrderRepository() : this(new OrderContext())
        {

        }

        public EFOrderRepository(OrderContext ctx)
        {
            this.ctx = ctx;
        }

        public bool Add(Order item)
        {
            var custom = ctx.Customers.Include(c => c.orders).FirstOrDefault(c => c.Id == item.IdCustomer);
            if (custom != null)
            {
                item.IdCustomer = custom.Id;
            }
            if (item == null)
            {
                return false;
            }
            
            ctx.Orders.Add(item);
            ctx.SaveChanges();
            return true;

        }

        public bool Delete(int id)
        {
            Order order = ctx.Orders.FirstOrDefault(o => o.Id == id);
            ctx.Orders.Remove(order);
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteOrder(string c)
        {
            Order order = ctx.Orders.FirstOrDefault(o => o.OrderCode == c);
            ctx.Orders.Remove(order);
            ctx.SaveChanges();
            return true;
        }

        public List<Order> FetchAll()
        {
            return ctx.Orders.ToList();
        }

        public Order GetByCode(string codiceOrdine)
        {
            Order order = ctx.Orders.FirstOrDefault(o => o.OrderCode == codiceOrdine);
            return order;

        }

        public Order GetById(int id)
        {
            return ctx.Orders.FirstOrDefault(o => o.Id == id);
        }

        public bool Update(Order item)
        {
            var oldOrder = ctx.Orders.FirstOrDefault(p => p.Id == item.Id);
            oldOrder.DateOfOrder = item.DateOfOrder;
            oldOrder.OrderCode = item.OrderCode;
            oldOrder.ProductCode = item.ProductCode;
            oldOrder.ToPay = item.ToPay;
            ctx.SaveChanges();
            return true;
        }
    }
}
