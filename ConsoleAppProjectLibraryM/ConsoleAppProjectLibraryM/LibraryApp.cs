using ConsoleAppProjectLibraryM.Model;
using ConsoleAppProjectLibraryM.Repository;
using ConsoleAppProjectLibraryM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppProjectLibraryM
{
    internal class LibraryApp
    {
        static async Task Main(string[] args)
        {
            IService BookService = new ServiceImplementation(new RepositoryImplementation());

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Book Management System");
                Console.WriteLine("1 -- Add Book details");
                Console.WriteLine("2 -- Search by  TITLE  ");
                Console.WriteLine("3 -- Search by  AUTHOR");
                Console.WriteLine("4 -- exit  ");
                Console.WriteLine("Enter the choice");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddBookDetails(BookService);
                        break;
                    case "2":
                        await SearchByTitle(BookService);
                        break;
                    case "3":
                        await SearchByAuthor(BookService);
                        break;
                    case "4":
                        exit = true;
                        break;

                }


            }
        }

        private static async Task AddBookDetails(IService service)
        {
            var book = new Book();
            Console.WriteLine("Enter the book details");
            for (int i = 0; i < 5; i++)
            {
                book.Details[i] = await EnterValidated(book.DetailHeader[i]);
            }
            await service.AddData(book);
        }
        private static async Task SearchByTitle(IService service)
        {
            var book = new Book();
            string Title = await EnterValidated("Title");
            book = await service.Search1(Title);
            await Display(book);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(" UPDATE AND DELETE WINDOW 1 \n Ender your choice : 1 -- Edit data  for Genre  2  -- edit data for  price  --  3  delete data  4-- previous window ");
                string choice1 = Console.ReadLine();
                switch (choice1)
                {

                    case "1":
                        string Genre = await EnterValidated("Genre");
                        await service.EditData1(book.Details[0], Genre);
                        Console.WriteLine("The details Updated");
                        book = await service.Search1(Title);
                        await Display(book);
                        break;
                    case "2":
                        string Price = await EnterValidated("Price");
                        await service.EditData2(book.Details[0],Price);
                        Console.WriteLine("The details Updated");
                        book = await service.Search1(Title);
                        await Display(book);
                        break;
                    case "3":
                        await service.DeleteData(book.Details[0]);
                        Console.WriteLine("The details successfully deleted");
                        break;
                    case "4":
                        exit = true;
                        break;



                }
            }

        } 
        private static async Task SearchByAuthor(IService service)
        {
            var book = new Book();
            string Author = await EnterValidated("Author");
            book = await service.Search2(Author);
            await Display(book);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(" UPDATE AND DELETE WINDOW 1 \n Ender your choice : 1 -- Edit data  for Title  2  -- edit data for  Author --  3  delete data  4-- previous window ");
                string choice1 = Console.ReadLine();
                switch (choice1)
                {

                    case "1":
                        string Title = await EnterValidated("Title");
                        await service.EditData1(book.Details[0], Title);
                        Console.WriteLine("The details Updated");
                        book = await service.Search2(Title);
                        await Display(book);
                        break;
                    case "2":
                        string author = await EnterValidated("Author");
                        await service.EditData2(book.Details[0], author);
                        Console.WriteLine("The details Updated");
                        book = await service.Search2(book.Details[0]);
                        await Display(book);
                        break;
                    case "3":
                        await service.DeleteData(book.Details[0]);
                        Console.WriteLine("The details successfully deleted");
                        break;
                    case "4":
                        exit = true;
                        break;



                }
            }

        }

        private static async Task<string> EnterValidated(string fieldnum)
        {
            Console.WriteLine($"Enter the {fieldnum}");
            string data = Console.ReadLine();
            return data;
        }
        private static async Task Display(Book book)
        {
            Console.WriteLine("Enter the details of the book");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{book.DetailHeader[i]} :  {book.Details[i]}");
            }
        }
    }
}
