using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Week4.EsFinale.Core.Models
{

    [DataContract]
    public class Customer
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public List<Order> orders { get; set; } = new List<Order>();

        //public Customer(int id, string customerCode, string name, string surname, List<Order> order)
        //{
        //    Id = id;
        //    CustomerCode = customerCode;
        //    Name = name;
        //    Surname = surname;
        //    orders = order;

        //}
        public Customer() { }

        internal bool UpdateCustomer(Customer editedCustomer, Customer customerCode)
        {
            throw new NotImplementedException();
        }
    }
}