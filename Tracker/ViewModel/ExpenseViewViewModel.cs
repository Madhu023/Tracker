using System.Collections.ObjectModel;
using System.Linq;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;

namespace Tracker.ViewModel
{
    class ExpenseViewViewModel : ViewModelBase
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
            get
            {
                return _expenseTypes;
            }
            set
            {
                _expenseTypes = value;
                this.OnPropertyChanged("ExpenseTypes");
            }
        }

        public ObservableCollection<Expense> Expenses
        {
            get
            {
                return _expenses;
            }
            set
            {
                _expenses = value;
                this.OnPropertyChanged("Expenses");
            }
        }

        public ExpenseViewViewModel()
        {
            _queryHandler = ExpenseQueryHandler.GetDBConnector();

            PopulateData();

            ExpenseTypes = new ObservableCollection<string>((from expense in Expenses
                                                             orderby expense.Type
                                                             select expense.Type).Distinct().ToList());
        }

        private void AddExpenseData()
        {
            Expenses.Clear();
            _queryHandler.Add(ExpenseInfo);
            PopulateData();
        }

        private void PopulateData()
        {
            ExpenseInfo = new Expense();
            Expenses = new ObservableCollection<Expense>(_queryHandler.GetData().OrderByDescending(var => var.Time));
        }
    }
}
