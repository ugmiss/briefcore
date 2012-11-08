using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using DomainModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using BusinessService.Data;
using Microsoft.Practices.Prism.Regions;

namespace ReportingModule.ViewModel
{
    public class ReportingViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Cmd { get; private set; }
        Report[] _Result;
        public Report[] Result
        {
            get { return _Result; }
            set
            {
                _Result = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Result"));
                }
            }
        }
        public ReportingViewModel()
        {
            Result = new BusinessService.ReportRepository().GetAllReports();
            Cmd = new DelegateCommand(Seaching);
        }
        public void Seaching()
        {
            Result = new BusinessService.ReportRepository().GetAllReports();
        }
        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
