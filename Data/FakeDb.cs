using FinansCepte.Models;

namespace FinansCepte.Data;

    public static class FakeDb
    {
        public static List<Transaction> Transactions = new List<Transaction>();

        static FakeDb()
        {
            Transactions.Add(new Transaction
            {
                Type = "Gider",
                Title = "Market Alışverişi",
                Amount = 450.00m,
                Date = DateTime.Now,
            });
            
            Transactions.Add(new Transaction
            {
                Type = "Gelir",
                Title = "Maaş Ödemesi",
                Amount = 25000.00m,
                Date = DateTime.Now.AddDays(-2)
            });
        }

        public static void Add(Transaction item)
        {
            Transactions.Add(item);
        }
    }