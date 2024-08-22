using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppProjectOrderM.Model;
using ConsoleAppProjectOrderM.Repository;
namespace ConsoleAppProjectOrderM.Service
{
    public class ServiceImplementation:IService
    {
        private readonly IRepository _datarepository;
        public ServiceImplementation(IRepository datarepository)
        {
            _datarepository = datarepository;
        }
        public async Task AddData(Order order)
        {
            await _datarepository.AddData(order);
        }
        public async Task EditData1(string code, string quantity)
        {
            await _datarepository.EditData1(code, quantity);
        }
        public async Task EditData2(string code, string price)
        {
            await _datarepository.EditData2(code, price);
        }
        public async Task DeleteData(string Code)
        {
            await _datarepository.DeleteData(Code);
        }
        public async Task<Order> Search1(string Code)
        {
           return await _datarepository.Search1(Code);
        }
        public async Task<Order> Search2(string CustomerName)
        {
           return await  _datarepository.Search2(CustomerName);
        }

    }
}
