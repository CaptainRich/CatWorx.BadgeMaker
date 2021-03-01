// A class of general worker methods.

// Import the packages needed.
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Drawing;



namespace CatWorx.BadgeMaker
{
    class Util
    {
        /////////////////////////////////////////////////////////////////////
        public static void PrintEmployees( List<Employee> employees )
        {
            // Dump out the list
            for (int i = 0; i < employees.Count; i++)
            {
                string template = "{0,-10}\t{1,-20}\t{2}";
                Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetName(), 
                                                employees[i].GetPhotoUrl()));

            }
        }


        /////////////////////////////////////////////////////////////////////
        public static void MakeCSV( List<Employee> employees )
        {
            // Verify that the 'data' directory exists, and if it doesn't, create it.
            if (!Directory.Exists("data")) 
            {
                // If not, create it
                Directory.CreateDirectory("data");

            }

            // Setup to write to the CSV file.  The 'using' keyword here sets up a 'temporary' 
            // resource, once this code runs the resource is removed from memory.
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                // Any code that needs the StreamWriter would go in here.  Start with the column headers.
                file.WriteLine( "ID,Name,PhotoURL" );

                // Now write out the employee information.
                for (int i = 0; i < employees.Count; i++)
                {
                    string template = "{0},{1},{2}";
                    file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetName(), 
                                                employees[i].GetPhotoUrl()));
                }
            }
        }  

        /////////////////////////////////////////////////////////////////////
        public static void MakeBadges( List<Employee> employees )
        {
            // Define the layout variables, used to position the information on the badge
            int BADGE_WIDTH           = 669;
            int BADGE_HEIGHT          = 1044;

            int COMPANY_NAME_START_X  = 0;
            int COMPANY_NAME_START_Y  = 110;
            int COMPANY_NAME_WIDTH    = 100;

            int PHOTO_START_X         = 184;
            int PHOTO_START_Y         = 215;
            int PHOTO_WIDTH           = 302;
            int PHOTO_HEIGHT          = 302;

            int EMPLOYEE_NAME_START_X = 0;
            int EMPLOYEE_NAME_START_Y = 560;
            int EMPLOYEE_NAME_WIDTH   = BADGE_WIDTH;
            int EMPLOYEE_NAME_HEIGHT  = 100;
             
            int EMPLOYEE_ID_START_X   = 0;
            int EMPLOYEE_ID_START_Y   = 690;
            int EMPLOYEE_ID_WIDTH     = BADGE_WIDTH;
            int EMPLOYEE_ID_HEIGHT    = 100;

            // Setup the styling (graphics) objects needed
            StringFormat format    = new StringFormat();
            format.Alignment       = StringAlignment.Center;
            int FONT_SIZE          = 32;
            Font font              = new Font( "Arial",  FONT_SIZE );
            Font monoFont          = new Font( "Courier New", FONT_SIZE );

            SolidBrush brush       = new SolidBrush( Color.Black );
            SolidBrush brushTxt    = new SolidBrush( Color.White );
            Rectangle companyRect  = new Rectangle(COMPANY_NAME_START_X, COMPANY_NAME_START_Y, BADGE_WIDTH,
                                                    COMPANY_NAME_WIDTH);
            Rectangle employeeRect = new Rectangle(EMPLOYEE_NAME_START_X, EMPLOYEE_NAME_START_Y, EMPLOYEE_NAME_WIDTH,
                                                    EMPLOYEE_NAME_HEIGHT);
            Rectangle IdRect       = new Rectangle(EMPLOYEE_ID_START_X, EMPLOYEE_ID_START_Y, EMPLOYEE_ID_WIDTH,
                                                    EMPLOYEE_ID_HEIGHT);


            // Setup a temporary instance of a WebClient (temporary because of 'using' )
            using( WebClient client = new WebClient() )
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    Stream employeeStream = client.OpenRead( employees[i].GetPhotoUrl() );
                    //Console.WriteLine(client.OpenRead(employees[i].GetPhotoUrl()).GetType());
                    Image photo = Image.FromStream( employeeStream );

                    Image background = Image.FromFile( "badge.png" );  // get the background/template
                    Image badge = new Bitmap( BADGE_WIDTH, BADGE_HEIGHT );

                    Graphics graphic = Graphics.FromImage( badge );    // convert the bitmap to a graphic object
                    graphic.DrawImage( background, new Rectangle( 0, 0, BADGE_WIDTH, BADGE_HEIGHT ) );  // add the background
                    graphic.DrawImage( photo, new Rectangle( PHOTO_START_X, PHOTO_START_Y, 
                                                             PHOTO_WIDTH, PHOTO_HEIGHT ) );  // add employee photo

                    // Add the company name
                    graphic.DrawString( employees[i].GetCompanyName(), font, brushTxt, companyRect,
                                        format );

                    // Add the employee name
                    graphic.DrawString( employees[i].GetName(), font, brush, employeeRect,
                                        format );

                    // Add the employee ID                                                              
                    graphic.DrawString( employees[i].GetId().ToString(), monoFont, brush, IdRect,
                                        format );

                    String template = "data/{0}_badge.png";
                    String badgeName = String.Format( template, employees[i].GetId() );
                    badge.Save( badgeName );

                }
            }
        }

    }  
}
