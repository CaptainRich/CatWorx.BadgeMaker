// A class of general (people) input methods.

// Import the packages needed.
using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        ///////////////////////////////////////////////////////////////////////
        public static List<Employee> GetEmployees()
        {
            // This function returns a list of strings.
            // Setup the 'list' 
            List<Employee> employees = new List<Employee>() ;

            // Prompt for additional employees
            while( true )
            {
                Console.WriteLine("Please enter a first name (blank to exit): ");

                // Get a name from the console and assign it to a variable, then add it to the list.
                string firstName = Console.ReadLine();

                // Exit loop if there is no input
                if( firstName == "" )
                {
                    break;
                }

                // Query for the remainder of the employee data
                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter ID: ");
                int id = Int32.Parse(Console.ReadLine());

                Console.Write("Enter Photo URL:");
                string photoUrl = Console.ReadLine();

                // Create a new Employee instance
                Employee currentEmployee  = new Employee( firstName, lastName, id, photoUrl );

                // If there is input, add it to the list
                employees.Add( currentEmployee );
            }   

            // Return the list of employees
            return employees;         
        }

        ///////////////////////////////////////////////////////////////////////
        public static List<Employee> GetFromAPI()
        {
            // This function returns a list of strings, obtained from the API endpoint.
            // Setup the 'list' 
            List<Employee> employees = new List<Employee>();
        

            using (WebClient client = new WebClient() )
            {
                string response = client.DownloadString( "https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse( response );

                // Console.WriteLine(json.SelectToken("results[0].name.first"));
                // Console.WriteLine(json.SelectToken("results[1].name.first"));
                // Console.WriteLine(json.SelectToken("results[2].name.first"));

                foreach( JToken token in json.SelectToken( "results" ) )
                {
                    // Now we can parse the JSON data
                    Employee emp = new Employee
                    (
                        token.SelectToken( "name.first" ).ToString(),
                        token.SelectToken( "name.last" ).ToString(),
                        Int32.Parse( token.SelectToken( "id.value" ).ToString().Replace("-", "" ) ),
                        token.SelectToken( "picture.large").ToString()
                    );

                    // Add this employee to the array
                    employees.Add( emp );
                }
            }


            return employees;

        }        
    }
}