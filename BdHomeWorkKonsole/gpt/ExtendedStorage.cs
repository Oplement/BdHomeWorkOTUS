using HW.Entities;
using System;

namespace HW.Storage
{

    public class ExtendedStorage
    {
        public static void AddTransaction(BankingDbContext context, int accountId, string transactionType, decimal amount)
        {
            // Проверка наличия счета
            var account = context.Accounts.Find(accountId);
            if (account == null)
            {
                Console.WriteLine("Добавление транзакции невозможно. Счет не найден.");
                return;
            }

            var transaction = new Transaction
            {
                AccountId = accountId,
                TransactionType = transactionType,
                Amount = amount,
                TransactionDate = DateTime.Now.ToUniversalTime()
            };

            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public static void AddAccountWithClient(BankingDbContext context, int clientId, string accountType, decimal initialBalance)
        {
            // Проверка наличия клиента
            var client = context.Clients.Find(clientId);
            if (client == null)
            {
                Console.WriteLine("Добавление счета невозможно. Клиент не найден.");
                return;
            }

            var account = new Account
            {
                ClientId = clientId,
                AccountType = accountType,
                Balance = initialBalance
            };

            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }

}
