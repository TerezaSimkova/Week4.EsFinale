using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.Wcf
{
    
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        Customer GetCustomerById(int id);

        [OperationContract]
        bool AddCustomer(Customer newCustomer);

        [OperationContract]
        bool UpdateCustomer(Customer updatedCustomer);

        [OperationContract]
        bool DeleteCustomerById(int id);
    }
}

