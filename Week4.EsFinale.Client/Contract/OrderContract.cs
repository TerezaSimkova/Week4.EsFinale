using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.Client.Contract
{
    public class OrderContract
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string ProductCode { get; set; }
        public decimal ToPay { get; set; }
        public Customer _customer { get; set; }
        public int IdCustomer { get; set; }
    }
}
