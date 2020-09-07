using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tracker.BO;
using Tracker.Common;
using Tracker.Model;
using System.Linq;

namespace Tracker.ViewModel
{
    class InvestmentViewViewModel : ViewModelBase
    {
        private Investment _investmentInfo;

        private IQueryHandler<Investment> _queryHandler;

        private RelayCommand _addInvestment;

        private ObservableCollection<Investment> _investments;

        public InvestmentViewViewModel()
        {
            _investmentInfo = new Investment();

            _queryHandler = InvestmentQueryHandler.GetDBConnector();

            _investments = new ObservableCollection<Investment>(_queryHandler.GetData().OrderByDescending(var => var.Time));
        }

        public Investment InvestmentInfo
        {
            get
            {
                return _investmentInfo;
            }

            set
            {
                _investmentInfo = value;
                OnPropertyChanged("InvestmentInfo");
            }
        }

        public RelayCommand AddInvestment
        {
            get
            {
                if (null == _addInvestment)
                {
                    _addInvestment = new RelayCommand(param => this.AddInvestmentData());
                }
                return _addInvestment;
            }
        }

        public ObservableCollection<Investment> Investments
        {
            get
            {
                return _investments;
            }

            set
            {
                _investments = value;
                this.OnPropertyChanged("Investments");
            }
        }

        private void AddInvestmentData()
        {
            _investments.Add(InvestmentInfo);
            _queryHandler.Add(InvestmentInfo);
            InvestmentInfo = new Investment();
        }
    }
}
