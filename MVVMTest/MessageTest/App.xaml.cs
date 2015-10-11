using MessageTest.Message;
using MessageTest.View;
using MessageTest.ViewModel;
using MyMVVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MessageTest
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ViewModelManager.Register<MainWindow, MainWindowViewModel, MainRegister>();
        }
    }
}
