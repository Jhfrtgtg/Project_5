using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppProjectOrderM.Model;
using ConsoleAppProjectOrderM.Repository;
using ConsoleAppProjectOrderM.Service;
using ConsoleAppProjectOrderM.Validation;
namespace ConsoleAppProjectOrderM
{
    public class OrderApp
    {
        static async Task Main(string[] args)
        {
            IService OrderService = new ServiceImplementation(new RepositoryImplementation());

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Order Management System");
                Console.WriteLine("1 -- Add Order details");
                Console.WriteLine("2 -- Search by  OrderID  ");
                Console.WriteLine("3 -- Search by  CustomerName");
                Console.WriteLine("4 -- exit  ");
                Console.WriteLine("Enter the choice");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddOrderDetails(OrderService);
                        break;
                    case "2":
                        await SearchByOrderID(OrderService);
                        break;
                    case "3":
                        await SearchByCustomerName(OrderService);
                        break;
                    case "4":
                        exit = true;
                        break;

                }


            }
        }

        public static  async Task AddOrderDetails(IService service)
        { OrderApp App = new OrderApp();
           
            var order = new Order();
            Console.WriteLine("Enter the order details");
            for (int i = 0; i < 5; i++)
            {
                order.Details[i] = await EnterValidated(order.DetailHeader[i]);
            }
            await service.AddData(order);
            
        }
        public static async Task SearchByOrderID(IService service)
        {
            var order = new Order();
            string OrderID = await EnterValidated("OrderID");
            order = await service.Search1(OrderID);
            Display(order);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(" UPDATE AND DELETE WINDOW 1 \n Ender your choice : 1 -- Edit data  for price  2  -- edit data for  --  3  delete data  4-- previous window ");
                string choice1 = Console.ReadLine();
                switch (choice1)
                {

                    case "1":
                        string Quantity = await EnterValidated("Quantity");
                        await service.EditData1(OrderID, Quantity);
                        Console.WriteLine("The details Updated");
                        order = await service.Search1(OrderID);
                        Display(order);
                        break;
                    case "2":
                        string TotalPrice = await EnterValidated("TotalPrice");
                        await service.EditData1(OrderID,TotalPrice);
                        Console.WriteLine("The details Updated");
                        order = await service.Search1(OrderID);
                        Display(order);
                        break;
                    case "3":
                        service.DeleteData(OrderID);
                        Console.WriteLine("The details successfully deleted");
                        break;
                    case "4":
                        exit = true;
                        break;



                }
            }

        }
        public static async Task<string> EnterValidated(string fieldnum)
        {

            Console.WriteLine($"Enter the {fieldnum}");
            string data = Console.ReadLine();
            return data;
        }

        public static async Task SearchByCustomerName(IService service)
        {
            var order = new Order();
            
            string CustomerName = await EnterValidated("CustomerName");
            order = await service.Search2(CustomerName);
            Display(order);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(" UPDATE AND DELETE WINDOW 1 \n Ender your choice : 1 -- Edit data  for Quantity  2  -- edit data for TotalPrice  --  3  delete data  4-- previous window ");
                string choice1 = Console.ReadLine();
                switch (choice1)
                {

                    case "1":
                        string Quantity = await EnterValidated("Quantity");
                        service.EditData1(order.Details[0], Quantity);
                        Console.WriteLine("The details Updated");
                        order = await service.Search2(CustomerName);
                        Display(order);
                        break;
                    case "2":
                        string TotalPrice = await EnterValidated("TotalPrice");
                        service.EditData1(order.Details[0],TotalPrice);
                        Console.WriteLine("The details Updated");
                        order = await service.Search2(order.Details[0]);
                        Display(order);
                        break;
                    case "3":
                        service.DeleteData(order.Details[0]);
                        Console.WriteLine("The details successfully deleted");
                        break;
                    case "4":
                        exit = true;
                        break;
                }
            }

        }

       
        public static async Task Display(Order order)
        {
            if (order != null)
            {
                Console.WriteLine(" details of the order");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{order.DetailHeader[i]} :  {order.Details[i]}");
                }
            }
        }
    }
}
