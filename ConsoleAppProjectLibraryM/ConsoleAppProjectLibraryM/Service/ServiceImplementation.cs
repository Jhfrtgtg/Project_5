using ConsoleAppProjectLibraryM.Model;
using ConsoleAppProjectLibraryM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppProjectLibraryM.Service
{
    public class ServiceImplementation:IService
    {

        private readonly IRepository _datarepository;
        public ServiceImplementation(IRepository datarepository)
        {
            _datarepository = datarepository;
        }
        public async Task AddData(Book book)
        {
            await _datarepository.AddData(book);
        }
        public async Task  EditData1(string code, string Genre)
        {
           await  _datarepository.EditData1(code,Genre);
        }
        public async Task EditData2(string code,string price)
        {
           await _datarepository.EditData2(code,price);
        }
        public async Task DeleteData(string Code)
        {
          await _datarepository.DeleteData(Code);
        }
        public async Task<Book> Search1(string Title)
        {
           return await _datarepository.Search1(Title);
        }
        public async Task<Book> Search2(string Author)
        {
           return await _datarepository.Search2(Author);
        }

    }
}
