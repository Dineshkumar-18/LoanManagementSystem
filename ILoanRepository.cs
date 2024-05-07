using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace LoanManagementSystem
{
    public interface ILoanRepository
    {
        bool ApplyLoan(Loan loan,Customer customer);
        int CalculateInterest(int loanId);
        void LoanStatus(int loanId,Customer customer);
        int CalculateEMI(int loanId);
        void LoanRepayment(int loanId, int amount);
        List<Loan> GetAllLoans();
        Loan GetLoanById(int loanId);
    }
    public class LoanRepositoryImpl : ILoanRepository
    {
        private List<Loan> loans = new List<Loan>();

       public bool ApplyLoan(Loan loan, Customer customer)
        {
            try
            {
                using (SqlConnection connection = DBUtil.GetDBConn())
                {

                    string checkCustomerQuery = "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerId";
                    SqlCommand checkCustomerCommand = new SqlCommand(checkCustomerQuery, connection);
                    checkCustomerCommand.Parameters.AddWithValue("@CustomerId", customer.CustomerID);
                    int customerCount = (int)checkCustomerCommand.ExecuteScalar();


                    if (customerCount == 0)
                    {
                        // Customer doesn't exist, insert new customer
                        string insertCustomerQuery = "INSERT INTO Customer (CustomerID, Name, EmailAddress, PhoneNumber, Address, CreditScore) VALUES (@CustomerId, @Name, @EmailAddress, @PhoneNumber, @Address, @CreditScore)";
                        SqlCommand insertCustomerCommand = new SqlCommand(insertCustomerQuery, connection);
                        insertCustomerCommand.Parameters.AddWithValue("@CustomerId", customer.CustomerID);
                        insertCustomerCommand.Parameters.AddWithValue("@Name", customer.Name);
                        insertCustomerCommand.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                        insertCustomerCommand.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        insertCustomerCommand.Parameters.AddWithValue("@Address", customer.Address);
                        insertCustomerCommand.Parameters.AddWithValue("@CreditScore", customer.CreditScore);
                        insertCustomerCommand.ExecuteNonQuery();
                    }



                    string query = "INSERT INTO Loan (LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) VALUES (@LoanId, @CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LoanId", loan.LoanId);
                    command.Parameters.AddWithValue("@CustomerId", loan.CustomerID);
                    command.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    command.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    command.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    command.Parameters.AddWithValue("@LoanType", loan.LoanType);
                    command.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        loans.Add(loan);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error applying for the loan: " + ex.Message);
                return false;
            }
        }
        public int CalculateInterest(int loanId)
        {

            var loan = GetLoanById(loanId);
            return (loan.PrincipalAmount * loan.InterestRate * loan.LoanTerm) / 12;
        }

        public void LoanStatus(int loanId, Customer customer)
        {

            var loan = GetLoanById(loanId);
            if (customer.CreditScore> 650)
            {
                loan.LoanStatus = "Approved";
                Console.WriteLine("Loan approved.");
            }
            else
            {
                loan.LoanStatus = "Rejected";
                Console.WriteLine("Loan rejected.");
            }
        }

        public int CalculateEMI(int loanId)
        {
            var loan = GetLoanById(loanId);
            int monthlyInterestRate = loan.InterestRate / 12 / 100;
            int  emi = (loan.PrincipalAmount * monthlyInterestRate * (int)Math.Pow(1 + monthlyInterestRate, loan.LoanTerm))
                          / ((int)Math.Pow(1 + monthlyInterestRate, loan.LoanTerm) - 1);
            return emi;
        }

        public void LoanRepayment(int loanId, int amount)
        {
            var loan = GetLoanById(loanId);
            int emi = CalculateEMI(loanId);
            if (emi == 0)
            {
                Console.WriteLine("Error: EMI calculation resulted in 0.");
                return;
            }
            if (amount < emi)
            {
                Console.WriteLine("Payment rejected. Amount is less than EMI.");
            }
            else
            {
                int noOfEmi = (amount / emi);
                Console.WriteLine($"Paid {noOfEmi} EMI(s).");
            }
        }

        public List<Loan> GetAllLoans()
        {
            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            var loan = loans.Find(l => l.LoanId == loanId);
            if (loan == null)
            {
                throw new Exception("Loan not found.");
            }
            return loan;
        }
    }
}
