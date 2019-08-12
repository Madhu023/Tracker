using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;

namespace Tracker.ViewModel
{
    class IncomeViewViewModel : ViewModelBase
    {
        private IQueryHandler<Income> _queryHandler;

        private ObservableCollection<Income> _income;

        private Income _incomeInfo;

        private ObservableCollection<string> _types;

        private RelayCommand _addIncome;

        public Income IncomeInfo
        {
            get
            {
                return _incomeInfo;
            }
            set
            {
                _incomeInfo = value;
                OnPropertyChanged("IncomeInfo");
            }
        }

        public RelayCommand AddIncome
        {
            get
            {
                if (null == _addIncome)
                {
                    _addIncome = new RelayCommand(param => this.AddIncomeData());
                }
                return _addIncome;
            }
        }

        public ObservableCollection<string> Types
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                OnPropertyChanged("Type");
            }
        }

        public ObservableCollection<Income> Income
        {
            get
            {
                return _income;
            }
            set
            {
                _income = value;
                OnPropertyChanged("Income");
            }
        }

        private void AddIncomeData()
        {
            _queryHandler.Add(IncomeInfo);
            UpdateData();
        }

        public IncomeViewViewModel()
        {
            _queryHandler = IncomeQueryHandler.GetDBConnector();
            _types = new ObservableCollection<string>(new List<string>(){ "USD", "INR"});
            IncomeInfo = new Income();
            Income = new ObservableCollection<Income>(_queryHandler.GetData());
        }

        private void UpdateData()
        {
            Income.Clear();
            Income = new ObservableCollection<Income>(_queryHandler.GetData());
        }
    }
}
