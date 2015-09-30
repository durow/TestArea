using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMVVM;

namespace MessageTest
{
    class VmManager:ViewModelManager
    {
        public object MainWindow
        {
            get
            {
                return new MainWindowViewModel();
            }
        }
    }
}
