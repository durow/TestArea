using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMVVM;

namespace MVVMTest
{
    class MainWindowViewModel:ViewModelBase
    {
        private double _doubleValue;

        public double DoubleValue
        {
            get { return _doubleValue; }
            set
            { 
                SetAndNotiryIfChanged("DoubleValue",ref _doubleValue, value);
            }
        }

    }
}
