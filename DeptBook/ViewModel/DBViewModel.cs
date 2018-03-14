using DeptBook.Model;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DeptBook.ViewModel
{
    class DBViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        DBModel dbModel = new DBModel();

        class Debitor
        {
            public string Name
            {
                get => dbModel.Name;    //dbModel.Debitor.Name; ?
                set
                {
                    if (value != dbModel.Name)
                    {
                        dbModel.Name = value;
                        OnPropertyChanged();
                    }

                }
            }
            int TotalBalance;
            int ID;
            int CalcBalance()
            {
                return 0;
            }
        }
        internal class Transaction
        {
            public int Amount
            {
                get => dbModel.Amount;  //dbModel.Transaction.Amount; ?
            }
        }

        Debitor debitor = new Debitor();
        
        //New Debitor
        private ICommand _addDebitorCommand;
        public ICommand AddDebitorCommand => 
            _addDebitorCommand ?? (_addDebitorCommand = new DelegateCommand(AddDebitor, AddDebitorCanExecute));

        private bool AddDebitorCanExecute()
        {
            return (!String.IsNullOrEmpty(debitor.Name));
        }

        private void AddDebitor()
        {
            throw new NotImplementedException();
        }

        private ICommand _newTransactionCommand;
        public ICommand NewTransactionCommand => 
            _newTransactionCommand ?? (_newTransactionCommand = new DelegateCommand(NewTransaction, NewTransactionCanExecute));

        private bool NewTransactionCanExecute()
        {
            throw new NotImplementedException();
        }

        private void NewTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
