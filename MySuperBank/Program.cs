
using BankLibrary;
using System;


namespace MySuperBank
{
    class Program
    {

        static void Main(string[] args)
        {

            var account1 = new BankAccount("Seb", 1000.50M);
            Console.WriteLine($"Account {account1.Number} was created for {account1.Owner} with {account1.Balance}.");

            account1.MakeWithdrawal(50, DateTime.Now, "Hammock");
            account1.MakeWithdrawal(50, DateTime.Now, "stuff");
            account1.MakeDeposit(2000, DateTime.Now, "Payday");
            account1.MakeWithdrawal(5, DateTime.Now, "small");
            account1.MakeWithdrawal(5, DateTime.Now, "small");
            account1.MakeWithdrawal(120, DateTime.Now, "check");
            account1.MakeDeposit(2, DateTime.Now, "small");

            Console.WriteLine(account1.GetAccountHistory());

            string yn;
            
            Console.WriteLine("Would you like to make a transaction? y/n");
            yn = Console.ReadLine();
            while (yn.Equals("y"))
            {
                account1.Teller();
                Console.WriteLine("Would you like to make another transaction? y/n");
                yn = Console.ReadLine();
            }

             
        }
    }
}
