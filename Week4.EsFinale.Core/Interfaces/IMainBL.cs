using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.Core.Interfaces
{
   public interface IMainBL
    {
        #region Orders
        //Metodi per gli ordini
        List<Order> FetchOrders();
        bool CreateOrder(Order newOrder);
        bool EditOrder(Order editedOrder, Order orderCode);
        bool DeleteOrder(int id);

        Order GetOrderById(int id);

        #endregion

        #region Customers
        //Metodi per i clienti
        List<Customer> FetchCustomers();
        bool CreateCustomer(Customer newCustomer);
        bool EditCustomer(Customer editedCustomer, Customer customerCode);
        bool DeleteCustomer(int id);
       
        Customer GetCustomerById(int id);
        Order GetOrderByCodice(string codiceOrdine);
        Customer GetCustomerByCodice(string codiceCustomer);
        #endregion
    }
}
