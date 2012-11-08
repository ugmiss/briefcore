using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using DomainModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace UserManageModule.ViewModel
{
    public class UserInfoViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Cmd { get; private set; }
        UserInfo[] _Result;
        public UserInfo[] Result
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
        public UserInfoViewModel()
        {
            Result = new BusinessService.UserRepository().GetAllUserInfos();
            Cmd = new DelegateCommand(Seaching);
        }
        public void Seaching()
        {
            Result = new BusinessService.UserRepository().GetAllUserInfos();
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
