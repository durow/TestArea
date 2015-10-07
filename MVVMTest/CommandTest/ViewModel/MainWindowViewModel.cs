using System;
using System.Windows;
using MyMVVM;

namespace CommandTest.ViewModel
{
    class MainWindowViewModel : NotifyObject
    {

        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                _canExecute = value;
                RaisePropertyChanged("CanExecute");
            }
        }


        private MyCommand _normalCommand;
        public MyCommand NormalCommand
        {
            get
            {
                if(_normalCommand == null)
                    _normalCommand = new MyCommand(
                        new Action<object>(
                            o => MessageBox.Show("这是个普通命令!")));
                return _normalCommand;
            }
        }

        private MyCommand _canExecuteCommand;
        public MyCommand CanExecuteCommand
        {
            get
            {
                if (_canExecuteCommand == null)
                    _canExecuteCommand = new MyCommand(
                        new Action<object>(
                            o => MessageBox.Show("命令可以执行！")),
                        new Func<object, bool>(
                            o => CanExecute));
                return _canExecuteCommand;
            }
        }

        private MyCommand _paramCommand;
        public MyCommand ParamCommand
        {
            get
            {
                if (_paramCommand == null)
                    _paramCommand = new MyCommand(
                        new Action<object>(
                            o => MessageBox.Show(o.ToString())),
                        new Func<object, bool>(
                            o => !string.IsNullOrEmpty(o.ToString())));
                return _paramCommand;
            }
        }
    }
}
