using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppProjectOrderM.Validation
{
    public class Validation
    {
        public static async Task<string> EnterValidated(string fieldnum)
        {
            Console.WriteLine($"Enter the {fieldnum}");
            string data = Console.ReadLine();
            return  data;
        }
    }
}
