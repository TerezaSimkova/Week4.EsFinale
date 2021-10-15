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

    }
}