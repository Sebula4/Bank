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
            report.AppendLine("Date\t\tAmount\t\tBalance\t\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t\t{balance}\t\t{item.Notes}");
            }

            return report.ToString();
        }

        public void Teller()
        {
            decimal money;
            string reason;
            string dw;

            Console.WriteLine("Type 'd' for deposit, 'w' for withdraw, or press 'enter' to close the window"); ;
            dw = Console.ReadLine();

            if (dw.Equals("d"))
            {
                Console.WriteLine("Enter the deposit amount greater than 0: ");
                do
                    money = Convert.ToDecimal(Console.ReadLine());
                while (money <= 0);

                Console.WriteLine("Enter the deposit note: ");
                reason = Console.ReadLine();

                MakeDeposit(money, DateTime.Now, reason);
                Console.WriteLine(GetAccountHistory());

            }
            else if (dw.Equals("w"))
            {
                Console.WriteLine("Enter the withdraw amount greater than 0: ");
                do
                    money = Convert.ToDecimal(Console.ReadLine());
                while (money <= 0);
                Console.WriteLine("Enter the withdraw note: ");
                reason = Console.ReadLine();

                MakeWithdrawal(money, DateTime.Now, reason);
                Console.WriteLine(GetAccountHistory());
            }
            else
            {
                Console.WriteLine("Exit? y/n");
                if (Console.ReadLine().Equals("y"))
                    Environment.Exit(1);

            }
        }
    }
}
