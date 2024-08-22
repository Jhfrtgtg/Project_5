using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppProjectOrderM.Model;
using ConsoleAppProjectOrderM.Repository;
namespace ConsoleAppProjectOrderM.Service
{
    public interface IService
    {
        Task AddData(Order order);
        Task EditData1(string code, string quantity);
        Task EditData2(string code, string price);
        Task DeleteData(string Code);
        Task<Order> Search1(string Code);
        Task<Order> Search2(string CustomerName);
    }
}
