using System;
using System.Collections.Generic;
using System.Text;

namespace Week4.EsFinale.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string ProductCode { get; set; }
        public decimal ToPay { get; set; }
        //ForeignKey
        public Customer _customer { get; set; }
        public int IdCustomer { get; set; }

        //public Order(int id, string ordercode, DateTime dateOrder, string productCode, decimal payment, Customer _cu, int idCu)
        //{
        //    Id = id;
        //    OrderCode = ordercode;
        //    DateOfOrder = dateOrder;
        //    ProductCode = productCode;
        //    ToPay = payment;
        //    _customer = _cu;
        //    IdCustomer = idCu;
        //}
        public Order() { }

    }
}
