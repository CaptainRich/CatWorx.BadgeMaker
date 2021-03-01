using System;
using System.Collections.Generic;   // needed for Dictionaries

namespace CatWorx.BadgeMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the List container for the employees
            List<Employee> employees = new List<Employee>();

            // Get the list of employees from either the console or the 
            // (random) API endpoint.



            Boolean response = false;

            while (!response)
            {
                Console.WriteLine( "Obtain employee data from the (C)onsole or (A)PI, or (E)xit?");
                string answer = Console.ReadLine();
                string ans = answer.ToUpper();

                if (ans == "C")
                {
                    response = true;
                    employees = PeopleFetcher.GetEmployees();
                }
                else if (ans == "A")
                {
                    response = true;
                    employees = PeopleFetcher.GetFromAPI();
                }
                else if (ans == "E")
                {
                    return;
                }                
                else
                {
                    Console.WriteLine( "Invalid response, please try again.");
                }
            };

            // Dump out the list
            Util.PrintEmployees( employees );

            // Save the employee list to a CSV file
            Util.MakeCSV( employees );

            // Create the badges 
            Util.MakeBadges( employees );

        }

    }
}
