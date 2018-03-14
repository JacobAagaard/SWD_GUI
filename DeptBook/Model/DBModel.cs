using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeptBook.Model
{
    class DBModel
    {
        private DeptBook dp;
        public DBModel()
        {
            dp = new DeptBook();
        }

        public void AddDebitor(Debitor debitor)
        {
            dp.AddDebitor(debitor.Name);
        }

        public void NewTransaction(int ID, Transaction transaction)
        {
            dp.NewTransaction(ID, transaction);
        }

        public void GetBalanceByID(int id)
        {
            dp.GetBalanceByID(id);
        }

        public List<Debitor> GetDebitorList()
        {
            return dp.debitors;
        }
    }

    class Debitor
    {
        public string Name { get; set; }
        private static int _idCount = 0;
        private int _id;

        public Debitor(string name)
        {
            Name = name;
            _id = ++_idCount;
        }

        public int GetID()
        {
            return _id;
        }
    }

    class DeptBook
    {
        private Dictionary<int, List<Transaction>> transactions;
        public List<Debitor> debitors { get; private set; }
        public DeptBook()
        {
            debitors = new List<Debitor>();
            transactions = new Dictionary<int, List<Transaction>>();
        }

        public void AddDebitor(string Name)
        {
            Debitor debitor = new Debitor(Name);
            debitors.Add(debitor);
            transactions.Add(debitor.GetID(), new List<Transaction>());
        }

        public void NewTransaction(int ID, Transaction transaction)
        {
            var transactionList = new List<Transaction>();
            transactions.TryGetValue(ID, out transactionList);
            transactionList.Add(transaction);
        }

        public int GetBalanceByID(int id)
        {
            int amount = 0;
            List<Transaction> transactionList;
            transactions.TryGetValue(id, out transactionList);
            foreach (var transaction in transactionList)
            {
                amount += transaction.Amount;
            }

            return amount;
        }

        public Debitor GetDebitorByID(int id)
        {
            var debitorByID = new Debitor("");
            foreach(var debitor in debitors)
            {
                if(debitor.GetID() == id)
                    debitorByID = debitor;
                else
                    Console.WriteLine("ID was not found in " + nameof(GetDebitorByID));
            }
            return debitorByID;
        }
    }

    class Transaction
    {
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
