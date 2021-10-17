using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Interfaces;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.Core.BusinessLayer
{
    public class MainBL : IMainBL
    {
        private readonly IOrderRepository orderRepo;
        private readonly ICustomerRepository customerRepo;

        public MainBL(IOrderRepository orderRepo, ICustomerRepository customerRepo
        )
        {
            this.orderRepo = orderRepo;
            this.customerRepo = customerRepo;
        }

        #region Customers
        //Metodi per i clienti 
        public List<Customer> FetchCustomers()
        {
            return customerRepo.FetchAll();
        }

        public bool CreateCustomer(Customer newCustomer)
        {
            return customerRepo.Add(newCustomer);
        }
        public bool EditCustomer(Customer editedCustomer)
        {
            return customerRepo.Update(editedCustomer);
        }

        public bool DeleteCustomer(int id)
        {
            return customerRepo.Delete(id);
        }

        public Customer GetCustomerById(int id)
        {
            return customerRepo.GetById(id);
        }
        #endregion

        #region Orders
        //Metodi per gli ordini
        public bool CreateOrder(Order newOrder)
        {
            return orderRepo.Add(newOrder);
        }

        public bool DeleteOrder(int id)
        {
            return orderRepo.Delete(id);
        }
        public bool EditOrder(Order editedOrder)
        {
            return orderRepo.Update(editedOrder);
        }

        public List<Order> FetchOrders()
        {
            return orderRepo.FetchAll();
        }

        public Order GetOrderById(int id)
        {
            return orderRepo.GetById(id);
        }

        public Order GetOrderByCodice(string codiceOrdine)
        {
            return orderRepo.GetByCode(codiceOrdine);
        }



        #endregion
    }
}

