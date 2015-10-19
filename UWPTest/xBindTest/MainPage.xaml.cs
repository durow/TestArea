using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace xBindTest
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel MainVM { get; set; } = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = MainVM;
        }

        private async void PopupButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog(MainVM.StringText);
            var r = await dlg.ShowAsync();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            MainVM.StringText += "a";
        }
    }
}
