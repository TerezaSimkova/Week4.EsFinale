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
            return ctx.Customers.Include(c => c.orders).ToList();
        }

        public Customer GetByCode(string codiceCustomer)
        {
            Customer customer = ctx.Customers.FirstOrDefault(c => c.CustomerCode == codiceCustomer);
            return customer;
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

        public bool UpdateCustomer(Customer editedCustomer, Customer customerCode)
        {
            var oldCustomer = ctx.Customers.FirstOrDefault(p => p.CustomerCode == customerCode.CustomerCode);
            oldCustomer.Name = editedCustomer.Name;
            oldCustomer.Surname = editedCustomer.Surname;
            oldCustomer.CustomerCode = customerCode.CustomerCode;
            customerCode.orders = oldCustomer.orders;
            ctx.SaveChanges();
            return true;
        }
    }
}
