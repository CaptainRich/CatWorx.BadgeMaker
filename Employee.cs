namespace CatWorx.BadgeMaker
{
    class Employee
    {
        private string FirstName;
        private string LastName;
        private int Id;
        private string PhotoUrl;

        // Define the constructor
        public Employee( string firstName, string lastName, int id, string photoUrl ) {
            FirstName = firstName;
            LastName  = lastName;
            Id        = id;
            PhotoUrl  = photoUrl;
        }

        // Return an employees full name
        public string GetName() {
            return FirstName + " " + LastName;
        }

        // Return the employee Id
        public int GetId() {
            return Id;
        }

        // Return the URL to the employee photo
        public string GetPhotoUrl() {
            // Use 'https://placekitten.com/300/300' for the sample URL.
            return PhotoUrl;
        }

        // Return the company name
        public string GetCompanyName() {
            return "Cat Worx";
        }
    }

}