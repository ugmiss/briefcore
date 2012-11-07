using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Media.Animation;

namespace ApplicationHost
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Cmd { get; private set; }
        public ShellViewModel()
        {
            Cmd = new DelegateCommand(SwitchModule);
        }
        public void SwitchModule()
        {

        }
    }
}
