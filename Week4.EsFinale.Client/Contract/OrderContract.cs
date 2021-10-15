using System;
using System.Collections.Generic;
using System.Text;

namespace Week4.EsFinale.Client.Contract
{
    public class OrderContract
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string ProductCode { get; set; }
        public decimal ToPay { get; set; }
        public int IdCustomer { get; set; }
    }
}
