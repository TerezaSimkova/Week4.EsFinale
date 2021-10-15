using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Week4.EsFinale.Core.Interfaces;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.EF.Repositories
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private readonly OrderContext ctx;

        public EFCustomerRepository() : this(new OrderContext())
        {

        }

        public EFCustomerRepository(OrderContext ctx)
        {
            this.ctx = ctx;
        }


        public bool Add(Customer item)
        {
            if (item == null)
            {
                return false;
            }
            ctx.Customers.Add(item);
            ctx.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Customer customer = ctx.Customers.FirstOrDefault(c => c.Id == id);
            ctx.Customers.Remove(customer);
            ctx.SaveChanges();
            return true;
        }

        public List<Customer> FetchAll()
        {
            return ctx.Customers.ToList();
            //Include(c => c.orders).
        }

        public Customer GetById(int id)
        {
            return ctx.Customers.FirstOrDefault(o => o.Id == id);
        }

        public bool Update(Customer item)
        {
            var oldCustomer = ctx.Customers.FirstOrDefault(p => p.Id == item.Id);
            oldCustomer.Name = item.Name;
            oldCustomer.Surname = item.Surname;
            oldCustomer.CustomerCode = item.CustomerCode;
            ctx.SaveChanges();
            return true;
        }
    }
}
