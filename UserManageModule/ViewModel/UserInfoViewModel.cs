using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Model;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace UserManageModule.ViewModel
{
    public class UserInfoViewModel : INotifyPropertyChanged
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
            Cmd = new DelegateCommand(Seaching);
        }
        public void Seaching()
        {
            Result = new BusinessService.UserRepository().GetAllUserInfos();
        }
    }
}
