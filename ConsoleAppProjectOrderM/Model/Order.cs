using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleAppProjectOrderM.Model 

{
    public class Order
    {
        public int propertynum { get; set; }
        public string[] DetailHeader = new string[5];
        public string[] Details = new string[5];
        public Order()
        {
           


            this.propertynum = 5;
            DetailHeader = new string[5] { "OrderID", "CustomerName", "ProductCode", "Quantity", "TotalPrice" };
        }
    }
}
