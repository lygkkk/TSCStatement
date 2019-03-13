using System;

namespace TscStatement.Abstract.Models
{
    public class OrderInfo
    {
        public string Category { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string Summary { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public double? PreviousBalance { get; set; }
        public double? IssuedAmount { get; set; }
        public double? PaymentAmount { get; set; }
        public double? CurrentBalance { get; set; }
    }
}