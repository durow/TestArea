using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyMVVM;

namespace MessageTest
{
    class MainWindowViewModel:ViewModelBase
    {
        private MyCommand _CmdTest;

        /// <summary>
        /// Gets the CmdTest.
        /// </summary>
        public MyCommand CmdTest
        {
            get
            {
                if (_CmdTest == null)
                    _CmdTest = new MyCommand(
                        CmdTestFunction,
                        CanExecuteCmdTest);
                return _CmdTest;
            }
        }

        private void CmdTestFunction(object parameter)
        {

        }

        private bool CanExecuteCmdTest(object parameter)
        {
            return true;
        }
    }
}
