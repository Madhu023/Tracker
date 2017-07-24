using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;


namespace Tracker.ViewModel
{
    class DataViewViewModel : ViewModelBase
    {
        private string _fileName;

        private RelayCommand _browseCommand;

        private ObservableCollection<Expense> _expenseData;

        private ObservableCollection<YearlyExpenseData> _yearWiseExpenses;

        private IQueryHandler _queryHandler;

        private Expense _selectedExpense;

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
                if (null == _browseCommand)
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
            get => _yearWiseExpenses;
            set
            {
                _yearWiseExpenses = value;
                this.OnPropertyChanged("YearWiseExpenses");
            }
        }

        private void LoadData()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            FileName = fileDialog.FileName;
            ExpenseData.Clear();
            ExpenseData = new ObservableCollection<Expense>(ExcelParser.Parse(FileName));

           QueryHandler.GetDBConnector().ImportDataBase(ExpenseData);
        }

        public DataViewViewModel()
        {
            FileName = "Select a File to Load";
            _queryHandler = QueryHandler.GetDBConnector();

            YearWiseExpenses = new ObservableCollection<YearlyExpenseData>();
            PopulateData();
        }


        private void PopulateData()
        {
            ExpenseData = new ObservableCollection<Expense>(_queryHandler.GetExpenseData());

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

        }
    }


}
