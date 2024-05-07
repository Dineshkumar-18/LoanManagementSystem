using System;


namespace LoanManagementSystem
{
   public class Customer
    {
        public Customer()
        {

        }
        public Customer(int cid,string name,string email,int phone,string address,int creditscore)
        {
            CustomerID = cid;
            Name = name;
            EmailAddress = email;
            PhoneNumber = phone;
            Address = address;
            CreditScore = creditscore;
        }

        public int CustomerID
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string EmailAddress
        {
            get; set;
        }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        public void DisplayCustomerDetails()
        {
            Console.WriteLine("Customer ID: " + CustomerID);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Email Address: " + EmailAddress);
            Console.WriteLine("Phone Number: " + PhoneNumber);
            Console.WriteLine("Address: " + Address);
            Console.WriteLine("Credit Score: " + CreditScore);
        }
    }
}

   
