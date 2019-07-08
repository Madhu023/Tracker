using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;

namespace Tracker.ViewModel
{
    class AddViewViewModel : ViewModelBase
    {
        private Expense _expenseInfo;

        private RelayCommand _addExpense;

        private IQueryHandler<Expense> _queryHandler;

        private ObservableCollection<string> _expenseTypes;

        private ObservableCollection<Expense> _expenses;


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

        public ObservableCollection<string> ExpenseTypes
        {
            get => _expenseTypes;
            set
            {
                _expenseTypes = value;
                this.OnPropertyChanged("ExpenseTypes");
            }
        }

        public ObservableCollection<Expense> Expenses
        {
            get => _expenses;
            set
            {
                _expenses = value;
                this.OnPropertyChanged("Expenses");
            }
        }

        public AddViewViewModel()
        {
            _queryHandler = QueryHandler.GetDBConnector();

            PopulateData();

            ExpenseTypes = new ObservableCollection<string>((from expense in Expenses
                                                             orderby expense.Type
                                                             select expense.Type).Distinct().ToList());
        }

        private void AddExpenseData()
        {
            _queryHandler.AddExpense(ExpenseInfo);
            PopulateData();
        }

        private void PopulateData()
        {
            ExpenseInfo = new Expense();
            Expenses = new ObservableCollection<Expense>(_queryHandler.GetExpenseData().OrderByDescending(var => var.Time));
        }
    }
}
