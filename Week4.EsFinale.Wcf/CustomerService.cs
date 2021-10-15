using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Week4.EsFinale.Core.BusinessLayer;
using Week4.EsFinale.Core.Models;
using Week4.EsFinale.EF.Repositories;

namespace Week4.EsFinale.Wcf
{
    public class CustomerService : ICustomerService
    {
        //Implementazione del Service Contract 
        //-> Ci saranno metodi che chiamano metodi del business layer.

        //Vedi fetch come esempio

        private readonly MainBL mainBusinessLayer;

        public CustomerService()
        {
            mainBusinessLayer = new MainBL(
                new EFOrderRepository(),
                new EFCustomerRepository()
            );
        }

        public bool AddCustomer(Customer newCustomer)
        {
            if (newCustomer == null)
            {
                return false;
            }
            return mainBusinessLayer.CreateCustomer(newCustomer);
        }

        public bool DeleteCustomerById(int id)
        {
            if (id > 0)
            {
                return mainBusinessLayer.DeleteCustomer(id);
            }
            return false;
        }

        public List<Customer> GetAllCustomers()
        {
            var result = mainBusinessLayer.FetchCustomers().ToList();
            return result;
        }

        public Customer GetCustomerById(int id)
        {
            return mainBusinessLayer.GetCustomerById(id);
        }

        public bool UpdateCustomer(Customer updatedCustomer)
        {
            return mainBusinessLayer.EditCustomer(updatedCustomer);
        }
    }
}
