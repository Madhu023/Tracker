using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;


namespace Tracker.ViewModel
{
    class DataViewViewModel : ViewModelBase
    {
        private string _fileName;

        private RelayCommand _browseCommand;

        private RelayCommand _filterCommand;

        private ObservableCollection<Expense> _expenseData;

        private ObservableCollection<YearlyExpenseData> _yearWiseExpenses;

        private DateTime _startDate;

        private DateTime _endDate;

        private IQueryHandler<Expense> _queryHandler;

        private Expense _selectedExpense;

        private string _totalValue;

        public string FileName
        {
            get { return _fileName; }

            set
            {
                _fileName = value;
                this.OnPropertyChanged("FileName");
            }
        }

        public RelayCommand BrowseCommand
        {
            get
            {
                if (_browseCommand == null)
                {
                    _browseCommand = new RelayCommand(param => this.LoadData());

                }
                return _browseCommand;
            }

        }

        public ObservableCollection<Expense> ExpenseData
        {
            get { return _expenseData; }
            set
            {
                _expenseData = value;
                this.OnPropertyChanged("ExpenseData");
            }
        }

        public Expense SelectedExpense
        {
            get { return _selectedExpense; }

            set
            {
                _selectedExpense = value;
                this.OnPropertyChanged("SelectedExpense");
            }
        }

        public ObservableCollection<YearlyExpenseData> YearWiseExpenses
        {
            get { return _yearWiseExpenses; }
            set
            {
                _yearWiseExpenses = value;
                this.OnPropertyChanged("YearWiseExpenses");
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set 
            {
                _startDate = value;
                this.OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                this.OnPropertyChanged("EndDate");
            }
        }

        public string TotalValue
        {
            get { return _totalValue; }
            set
            {
                _totalValue = value;
                this.OnPropertyChanged("TotalValue");
            }
        }

        internal RelayCommand FilterCommand
        {
            get
            {
                if(_filterCommand == null)
                {
                    _filterCommand = new RelayCommand(param => this.FilterData());
                }
                return _filterCommand;
            }
        }

        private async void LoadData()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            FileName = fileDialog.FileName;
            ExpenseData.Clear();
            YearWiseExpenses.Clear();

            var Result = await ExcelParser.Parse(FileName);

            ExpenseData = new ObservableCollection<Expense>(Result);

           await  Task.Run(() =>
            {
                QueryHandler.GetDBConnector().ImportDataBase(ExpenseData);
            });

            PopulateData();
        }

        public DataViewViewModel()
        {
            FileName = "Select a File to Load";
            _queryHandler = QueryHandler.GetDBConnector();

            YearWiseExpenses = new ObservableCollection<YearlyExpenseData>();

            PopulateData();

            StartDate = EndDate = DateTime.Today;

        }

        private void PopulateData()
        {
            ExpenseData = new ObservableCollection<Expense>(_queryHandler.GetExpenseDataByCategory());

            TotalValue = ExpenseData.Sum(var => var.Amount).ToString();
            //await Task.Run(() =>
            {
                var yearlyExpenses = ExpenseData.GroupBy(year => year.Time.Year).ToList();

                foreach (var yearlyExpense in yearlyExpenses)
                {
                    YearlyExpenseData yearlyExpenseData = new YearlyExpenseData();

                    yearlyExpenseData.Year = yearlyExpense.Key;

                    var monthlyExpenseDetails = yearlyExpense.GroupBy(month => month.Time.ToString("MMM")).ToList();


                    foreach (var monthlyExpense in monthlyExpenseDetails)
                    {
                        yearlyExpenseData.MonthlyData.Add(new MonthlyExpenseData() { Month = monthlyExpense.Key, Amount = monthlyExpense.Sum(var => var.Amount) });
                    }

                    yearlyExpenseData.TotalExpense = yearlyExpenseData.MonthlyData.Sum(var => var.Amount);

                    YearWiseExpenses.Add(yearlyExpenseData);
                }
                //});
            }
        }


        private void FilterData()
        {
            ExpenseData.Clear();
            TotalValue = string.Empty;
        }
    }
}
