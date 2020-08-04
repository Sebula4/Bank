using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance { 
            get 
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        
        }

        private static int accountNumberSeed = 1234567890;

        private List<Transaction> allTransactions = new List<Transaction> ();

        public BankAccount(string name, decimal initialBalance) //Constructor
        {
            this.Owner = name;

            MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");

            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public void Ledger() //my attempt
        {
            foreach (var item in allTransactions)
            {
                if (item.Amount < 0)
                    Console.WriteLine($"Withdrew {item.Amount} on {item.Date} for {item.Notes}");
                else
                    Console.WriteLine($"Deposited {item.Amount} on {item.Date} for {item.Notes}");
            }
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;

            //header
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.AmountForHumans}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }

    }
}
