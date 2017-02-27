using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;

namespace Tracker.ViewModel
{
    class DataViewViewModel:ViewModelBase
    {
        private string _fileName;

        private RelayCommand _browseCommand;

        private ObservableCollection<Expense> _expenseData;
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

        private void LoadData()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            FileName = fileDialog.FileName;
            ExpenseData = new ObservableCollection<Expense>(ExcelParser.Parse(FileName));
        }

        public DataViewViewModel()
        {
            FileName = "Select a File to Load";
            ExpenseData = new ObservableCollection<Expense>();
        }
    }
}
