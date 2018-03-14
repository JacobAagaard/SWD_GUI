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
        Debitor debitor = new Debitor("");
        Transaction transaction = new Transaction();
        
        public string Name
        {
            get => debitor.Name;
            set
            {
                debitor.Name = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => transaction.Amount;
            set
            {
                transaction.Amount = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addDebitorCommand;
        public ICommand AddDebitorCommand => 
            _addDebitorCommand ?? (_addDebitorCommand = new DelegateCommand(AddDebitor, AddDebitorCanExecute));

        private bool AddDebitorCanExecute()
        {
            return (!String.IsNullOrEmpty(debitor.Name));   //Debitor name must me entered before adding.
        }

        private void AddDebitor()
        {
            dbModel.AddDebitor(debitor);
            OnPropertyChanged("AddDebitor");
        }

        private ICommand _newTransactionCommand;
        public ICommand NewTransactionCommand => 
            _newTransactionCommand ?? (_newTransactionCommand = new DelegateCommand(NewTransaction, NewTransactionCanExecute));

        private bool NewTransactionCanExecute()
        {
            return (transaction.Amount != 0);    // The transaction amount can be +/-, but not 0.
        }

        private void NewTransaction()
        {
            foreach (var debitor in dbModel.GetDebitorList())
            {
                if(this.debitor.GetID() == debitor.GetID()) //Virker måske ikke. Har endnu ikke testet.
                    dbModel.NewTransaction(debitor.GetID(), transaction);                    

                OnPropertyChanged("NewTransaction");
            }
        }
    }
}
