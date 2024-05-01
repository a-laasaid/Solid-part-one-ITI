using System;

namespace SOLID
{
    internal class Program
    {
        // TASK 1 SRP


        //       1- Identify Responsibilities:
        //          * Managing employee data(name, salary, department)
        //          * Calculating yearly salary
        //          * Generating reports
        //          * Sending email notifications
        //        2-Create Separate Classes:
        //          * EmployeeData class to manage employee data
        //          * SalaryCalculator class to calculate yearly salary
        //          * ReportGenerator class to generate reports
        //          * NotificationService class to send email notifications
        //      3- Refactor Employee Class:
        //          * Remove methods related to calculating salary, generating reports, and sending notifications.
        //          * Use instances of the new classes to perform these tasks.
        //      


        public class EmployeeData
        {
            public string Name { get; set; }
            public decimal Salary { get; set; }
            public string Department { get; set; }
        }

        public class SalaryCalculator
        {
            public decimal CalculateYearlySalary(EmployeeData employeeData)
            {
                return employeeData.Salary * 12;
            }
        }

        public class ReportGenerator
        {
            public void GenerateReport(string reportType, EmployeeData employeeData)
            {
                // Code
            }
        }

        public class NotificationService
        {
            public void SendNotification(string recipient, string message)
            {
                // Code 
            }
        }
        public class Employee
        {
            private EmployeeData employeeData;

            public Employee(EmployeeData data)
            {
                employeeData = data;
            }

            public decimal CalculateYearlySalary(SalaryCalculator calculator)
            {
                return calculator.CalculateYearlySalary(employeeData);
            }

            //generate report using ReportGenerator
            public void GenerateReport(ReportGenerator reportGenerator, string reportType)
            {
                reportGenerator.GenerateReport(reportType, employeeData);
            }

            //send notification using NotificationService
            public void SendNotification(NotificationService notificationService, string recipient, string message)
            {
                notificationService.SendNotification(recipient, message);
            }


            // TASK 2 OCP

            // Varying parts: there are different payment methods (credit card, PayPal, bank transfer).
            // Stable parts: The overall structure of the payment processor and its method signature.

            //2 - create an interface IPaymentMethod to represent different payment methods. Each payment method will implement this interface.

            public interface IPaymentMethod
            {
                void Process(double amount);
            }

            public class CreditCardPayment : IPaymentMethod
            {
                public void Process(double amount)
                {
                    //code of Process credit card payment
                }
            }

            public class PayPalPayment : IPaymentMethod
            {
                public void Process(double amount)
                {
                }
            }
            public class BankTransferPayment : IPaymentMethod
            {
                public void Process(double amount)
                {
                    // Process bank transfer payment
                }
            }
            public enum PaymentType
            {
                CreditCard,
                PayPal,
                BankTransfer
            }

            public class PaymentProcessor
            {
                /// <summary>
                /// Processes a payment using the specified payment method.
                /// </summary>
                /// <param name="paymentMethod">The payment method to be used for processing.</param>
                /// <param name="amount">The amount to be processed.</param>
                public void ProcessPayment(IPaymentMethod paymentMethod, double amount)
                {
                    paymentMethod.Process(amount);
                }
            }


        }


        //TASK 3 LSP

        //The Liskov Substitution Principle(LSP) states that objects of a superclass should be replaceable with
        //objects of a subclass without affecting the correctness of the program.
        //In the provided code, the SavingsAccount class violates the LSP because it alters the behavior of
        //the Withdraw method from its base class Account.

        public class Account
        {
            public decimal Balance { get; set; }

            // Deposit money into the account
            public virtual void Deposit(decimal amount)
            {
                Balance += amount;
            }
            public virtual void Withdraw(decimal amount)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                }
                else
                {
                    throw new InvalidOperationException("Insufficient balance.");
                }
            }
        }

        public class SavingsAccount : Account
        {
            public decimal InterestRate { get; set; }

            // Override the Withdraw method to include a withdrawal fee for SavingsAccount
            public override void Withdraw(decimal amount)
            {
                // Include a withdrawal fee for SavingsAccount
                decimal totalWithdrawalAmount = amount + 5.0m;

                // Check if there's enough balance to cover both withdrawal and fee
                if (Balance >= totalWithdrawalAmount)
                {
                    Balance -= totalWithdrawalAmount;
                }
                else
                {
                    throw new InvalidOperationException("Insufficient balance.");
                }
            }
        }

    }
}
