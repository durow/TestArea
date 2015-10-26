using MyMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EventBindingTest
{
    class MainWindowViewModel : NotifyObject
    {

        private bool _IsReceiveMouseMove = true;
        public bool IsReceiveMouseMove
        {
            get { return _IsReceiveMouseMove; }
            set
            {
                _IsReceiveMouseMove = value;
                RaisePropertyChanged("IsReceiveMouseMove");
            }
        }

        private string _tipText;
        public string TipText
        {
            get { return _tipText; }
            set
            {
                _tipText = value;
                RaisePropertyChanged("TipText");
            }
        }


        private MyCommand _loadedCommand;
        public MyCommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                    _loadedCommand = new MyCommand(
                        new Action<object>(
                            o => MessageBox.Show("程序加载完毕！")));
            return _loadedCommand;
            }
        }

        private MyCommand<MouseEventArgs> _mouseMoveCommand;
        public MyCommand<MouseEventArgs> MouseMoveCommand
        {
            get
            {
                if (_mouseMoveCommand == null)
                    _mouseMoveCommand = new MyCommand<MouseEventArgs>(
                        new Action<MouseEventArgs>(e =>
                        {
                            var point = e.GetPosition(e.Device.Target);
                            var left = "左键放开";
                            var mid = "中键放开";
                            var right = "右键放开";

                            if (e.LeftButton == MouseButtonState.Pressed)
                                left = "左键按下";
                            if (e.MiddleButton == MouseButtonState.Pressed)
                                mid = "中键按下";
                            if (e.RightButton == MouseButtonState.Pressed)
                                right = "右键按下";

                            TipText = $"当前鼠标位置  X:{point.X}  Y:{point.Y}  当前鼠标状态:{left} {mid}  {right}";
                        }),
                        new Func<object, bool>(o => IsReceiveMouseMove));
                return _mouseMoveCommand;
            }
        }
    }
}
