using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Common;
using Tracker.Model;

namespace Tracker.ViewModel
{
    class AddViewViewModel : ViewModelBase
    {

        Expense _expenseInfo;

        RelayCommand _addExpense;

        public Expense ExpenseInfo
        {
            get { return _expenseInfo; }
            set
            {
                _expenseInfo = value;
                this.OnPropertyChanged("ExpenseInfo");
            }
        }

        public RelayCommand AddExpense
        {
            get
            {
                if (null == _addExpense)
                {
                    _addExpense = new RelayCommand(param => this.AddExpenseData());
                }
                return _addExpense;
            }
        }

        private void AddExpenseData()
        {
            ExpenseInfo = null;
            ExpenseInfo = new Expense();

            throw new NotImplementedException();
        }

        public AddViewViewModel()
        {
            ExpenseInfo = new Expense();
        }
    }
}
