using System;


namespace LoanManagementSystem
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int CustomerID { get; set; }
        public int PrincipalAmount { get; set; }
        public int InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan()
        {

        }

        public Loan(int loanId, int customerId, int principalAmount, int interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            CustomerID = customerId;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }
        public void PrintLoanDetails()
        {
           
            Console.WriteLine($"Loan ID: {LoanId}");
            Console.WriteLine($"Customer ID: {CustomerID}");
            Console.WriteLine($"Principal Amount: {PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {InterestRate}");
            Console.WriteLine($"Loan Term: {LoanTerm}");
            Console.WriteLine($"Loan Type: {LoanType}");
            Console.WriteLine($"Loan Status: {LoanStatus}");
        }
    }
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public int PropertyValue { get; set; }
        public HomeLoan() { }

        public HomeLoan(int loanId, int customerId, int principalAmount, int interestRate, int loanTerm, string loanType, string loanStatus, string propertyAddress, int propertyValue)
            : base(loanId, customerId, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public void PrintHomeLoanDetails()
        {

            Console.WriteLine($"Loan ID: {LoanId}");
            Console.WriteLine($"Customer ID: {CustomerID}");
            Console.WriteLine($"Principal Amount: {PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {InterestRate}");
            Console.WriteLine($"Loan Term: {LoanTerm}");
            Console.WriteLine($"Loan Type: {LoanType}");
            Console.WriteLine($"Loan Status: {LoanStatus}");
            Console.WriteLine($"Home Property Address: {PropertyAddress}");
            Console.WriteLine($"Home Property Value: {PropertyValue}");
        }


    }

    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public int CarValue { get; set; }
        public CarLoan() { }

        public CarLoan(int loanId, int customerId, int principalAmount, int interestRate, int loanTerm, string loanType, string loanStatus, string carModel, int carValue)
            : base(loanId, customerId, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }
        public void PrintCarLoanDetails()
        {

            Console.WriteLine($"Loan ID: {LoanId}");
            Console.WriteLine($"Customer ID: {CustomerID}");
            Console.WriteLine($"Principal Amount: {PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {InterestRate}");
            Console.WriteLine($"Loan Term: {LoanTerm}");
            Console.WriteLine($"Loan Type: {LoanType}");
            Console.WriteLine($"Loan Status: {LoanStatus}");
            Console.WriteLine($"Car Model: {CarModel}");
            Console.WriteLine($"Car Value: {CarValue}");
        }
    }

    
}