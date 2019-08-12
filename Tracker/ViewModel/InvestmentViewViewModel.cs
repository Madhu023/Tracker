using System;
using Tracker.Common;
using Tracker.Model;

namespace Tracker.ViewModel
{
    class InvestmentViewViewModel : ViewModelBase
    {
        Investment _investmentInfo;

        RelayCommand _addInvestment;

        public InvestmentViewViewModel()
        {
            _investmentInfo = new Investment();
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

        private void AddInvestmentData()
        {
            throw new NotImplementedException();
        }
    }
}
