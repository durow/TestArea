using AyxMVVM;
using AyxMVVM.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace xBindTest
{
    public class MainPageViewModel:ObserveObject
    {
        private string _StringText = "1234";

        public string StringText
        {
            get { return _StringText; }
            set
            {
                if (_StringText != value)
                {
                    _StringText = value;
                    RaisePropertyChanged("StringText");
                }
            }
        }


        private AyxCommand _CmdChange;

        /// <summary>
        /// Gets the CmdChange.
        /// </summary>
        public AyxCommand CmdChange
        {
            get
            {
                if (_CmdChange == null)
                    _CmdChange = new AyxCommand(
                    o =>
                    {
                        StringText += "a";
                    });
                return _CmdChange;
            }
        }

        private AyxCommand _CmdPopup;

        private bool _CanPopup;


        /// <summary>
        /// Gets the CmdPopup.
        /// </summary>
        public AyxCommand CmdPopup
        {
            get
            {
                if (_CmdPopup == null)
                    _CmdPopup = new AyxCommand(
                    async o =>
                    {
                        var dlg = new MessageDialog(StringText);
                        var r = await dlg.ShowAsync();
                    });
                return _CmdPopup;
            }
        }

        public async void PopupFunction(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog(StringText);
            var r = await dlg.ShowAsync();
        }

        private AyxCommand _CmdLoaded;

        /// <summary>
        /// Gets the CmdLoaded.
        /// </summary>
        public AyxCommand CmdLoaded
        {
            get
            {
                if (_CmdLoaded == null)
                    _CmdLoaded = new AyxCommand(
                    async o =>
                    {
                        var dlg = new MessageDialog(o.ToString ());
                        var r = await dlg.ShowAsync();
                    });
                return _CmdLoaded;
            }
        }
    }
}
