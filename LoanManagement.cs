using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem
{
    internal class LoanManagement
    {
        private static ILoanRepository loanRepository = new LoanRepositoryImpl();
        public static void Main(string[] args)
        {
            Console.WriteLine("Loan Management System");
            Console.WriteLine("1. Apply Loan");
            Console.WriteLine("2. Get All Loans");
            Console.WriteLine("3. Get Loan By ID");
            Console.WriteLine("4. Loan Repayment");
            Console.WriteLine("5. Exit");

            while (true)
            {
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ApplyLoan();
                        break;
                    case "2":
                        GetAllLoans();
                        break;
                    case "3":
                        GetLoanById();
                        break;
                    case "4":
                        LoanRepayment();
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private static void ApplyLoan()
        {
            Console.WriteLine("Applying for a loan...");

            Console.Write("Enter Customer ID: ");
            int customerId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email Address: ");
            string emailAddress = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            int phoneNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Credit Score: ");
            int creditScore = Convert.ToInt32(Console.ReadLine());

            Customer customer = new Customer(customerId, name, emailAddress, phoneNumber, address, creditScore);


            // loan details
            Console.Write("Enter Loan ID: ");
            int loanId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Customer ID: ");
            int cusId= Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Principal Amount: ");
            int principalAmount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Interest Rate: ");
            int interestRate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Loan Term (in months): ");
            int loanTerm = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Loan Type (CarLoan/HomeLoan): ");
            string loanType = Console.ReadLine();

            Loan loan;
            if (loanType.Equals("CarLoan"))
            {
                Console.Write("Enter Car Model: ");
                string carModel = Console.ReadLine();

                Console.Write("Enter Car Value: ");
                int carValue = Convert.ToInt32(Console.ReadLine());

                loan = new CarLoan(loanId, cusId, principalAmount, interestRate, loanTerm, loanType, "Pending", carModel, carValue);
            }
            else if (loanType.Equals("HomeLoan"))
            {
                Console.Write("Enter Property Address: ");
                string propertyAddress = Console.ReadLine();

                Console.Write("Enter Property Value: ");
                int propertyValue = Convert.ToInt32(Console.ReadLine());

                loan = new HomeLoan(loanId, cusId, principalAmount, interestRate, loanTerm, loanType, "Pending", propertyAddress, propertyValue);
            }
            else
            {
                Console.WriteLine("Invalid Loan Type.");
                return;
            }

            bool result = loanRepository.ApplyLoan(loan,customer);
            if (result)
            {
                Console.WriteLine("Loan applied successfully.");
            }
            else
            {
                Console.WriteLine("Failed to apply for the loan.");
            }
        }
        private static void GetAllLoans()
        {
            Console.WriteLine("Fetching all loans...");
            var loans = loanRepository.GetAllLoans();
            foreach (var loan in loans)
            {
                loan.PrintLoanDetails();
            }
        }
        private static void GetLoanById()
        {
            Console.WriteLine("Enter loan ID: ");
            int loanId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var loan = loanRepository.GetLoanById(loanId);
                loan.PrintLoanDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void LoanRepayment()
        {
            Console.WriteLine("Enter loan ID: ");
            int loanId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount for repayment: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            loanRepository.LoanRepayment(loanId, amount);
        }
    }
}
