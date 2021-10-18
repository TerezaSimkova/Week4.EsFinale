using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.Client.Contract
{
    public class CustomerContract
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Order> orders { get; set; } = new List<Order>();
    }
}
