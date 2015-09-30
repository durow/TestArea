using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVVMTest.Message;
using MVVMTest.ViewModel;
using MyMVVM;

namespace MVVMTest.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModelManager.RegisterViewModel(
                this, 
                new MainWindowViewModel(), 
                new MainWindowMsg());
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            MessageManager.Default.SendMsg("LoadingRow", e);
        }
    }
}
