using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using MVVMTest.Model;
using MyMVVM;

namespace MVVMTest.ViewModel
{
    class MainWindowViewModel:ViewModelBase
    {

        private TestData _selectedData;

        public TestData SelectedData
        {
            get { return _selectedData; }
            set
            {
                SetAndNotifyIfChanged("SelectedData", ref _selectedData, value);
            }
        }

        private ObservableCollection<TestData> _testDataList;

        public ObservableCollection<TestData> TestDataList
        {
            get { return _testDataList; }
            set
            {
                _testDataList = value;
                SetAndNotifyIfChanged("TestDataList", ref _testDataList, value);
            }
        }

        public MainWindowViewModel()
        {
            TestDataList = new ObservableCollection<TestData>(TestData.GetTestData());
            RegisterMessage();
        }

        private MyCommand _cmdAdd;

        public MyCommand CmdAdd
        {
            get
            {
                if (_cmdAdd == null)
                    _cmdAdd = new MyCommand(new Action<object>
                    (
                        o =>
                        {
                            var data = TestData.GetNew();
                            TestDataList.Add(data);
                            SelectedData = data;
                        }
                    ));
                return _cmdAdd;
            }
        }

        private MyCommand _cmdEdit;

        public MyCommand CmdEdit
        {
            get
            {
                if (_cmdEdit == null)
                    _cmdEdit = new MyCommand(new Action<object>
                    (
                        o =>
                        {

                        }
                    ),
                    new Func<object, bool>(o=>SelectedData != null));
                return _cmdEdit;
            }
        }

        private MyCommand _CmdDelete;

        public MyCommand CmdDelete
        {
            get
            {
                if (_CmdDelete == null)
                    _CmdDelete = new MyCommand(new Action<object>
                    (
                        o =>
                        {
                            TestDataList.Remove(SelectedData);
                        }
                    ),
                    new Func<object, bool>(o => SelectedData != null));
                return _CmdDelete;
            }
        }

        private MyCommand _cmdLoadingRow;

        public MyCommand CmdLoadingRow
        {
            get
            {
                if (_cmdLoadingRow == null)
                    _cmdLoadingRow = new MyCommand(new Action<object>
                    (
                        o =>
                        {
                            MsgManager.SendMsg<string>("ShowBox","test");
                            //var data = e.Row.Item as TestData;
                            //if (data == null) return;
                            //if (data.IntValue < 1200)
                            //    e.Row.Background = Brushes.LightSteelBlue;
                            //else if (data.IntValue < 1500)
                            //    e.Row.Background = Brushes.CornflowerBlue;
                            //else if (data.IntValue < 1800)
                            //    e.Row.Background = Brushes.Blue;
                            //else
                            //    e.Row.Background = Brushes.Navy;
                        }
                    ));
                return _cmdLoadingRow;
            }
        }

        private void RegisterMessage()
        {
            MsgManager.Register(this, "LoadingRow", new Action<DataGridRowEventArgs>(e =>
            {
                var data = e.Row.Item as TestData;
                if (data == null) return;
                if (data.IntValue < 2000)
                    e.Row.Background = Brushes.LightSteelBlue;
                else if (data.IntValue < 5000)
                    e.Row.Background = Brushes.CornflowerBlue;
                else if (data.IntValue < 8000)
                    e.Row.Background = Brushes.LightPink;
                else
                    e.Row.Background = Brushes.LightGreen;
            }));
        }
    }
}
