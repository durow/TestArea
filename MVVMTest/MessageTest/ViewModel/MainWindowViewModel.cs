using MessageTest.Message;
using MyMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MessageTest.ViewModel
{
    class MainWindowViewModel:ViewModelBase
    {
        private int _NumOne;

        public int NumOne
        {
            get { return _NumOne; }
            set
            {
                SetAndNotifyIfChanged("NumOne", ref _NumOne, value);
            }
        }
        private int _NumTwo;

        public int NumTwo
        {
            get { return _NumTwo; }
            set
            {
                SetAndNotifyIfChanged("NumTwo", ref _NumTwo, value);
            }
        }
        private int _NumResult;

        public int NumResult
        {
            get { return _NumResult; }
            set
            {
                SetAndNotifyIfChanged("NumResult", ref _NumResult, value);
            }
        }

        private Brush _BgBrush;

        public Brush BgBrush
        {
            get { return _BgBrush; }
            set
            {
                SetAndNotifyIfChanged("BgBrush", ref _BgBrush, value);
            }
        }


        private MyCommand _CmdNormal;

        /// <summary>
        /// Gets the CmdNormal.
        /// </summary>
        public MyCommand CmdNormal
        {
            get
            {
                if (_CmdNormal == null)
                    _CmdNormal = new MyCommand(
                    o =>
                    {
                        MsgManager.SendMsg("ShowBox", "这是提示");
                    });
                return _CmdNormal;
            }
        }

        private MyCommand _CmdConfirm;

        /// <summary>
        /// Gets the CmdConfirm.
        /// </summary>
        public MyCommand CmdConfirm
        {
            get
            {
                if (_CmdConfirm == null)
                    _CmdConfirm = new MyCommand(
                    o =>
                    {
                        var msg = new ConfirmMsgArgs(
                            "请确认!",
                            "是否要把当前按钮颜色变成蓝色？");
                        MsgManager.SendMsg("ShowConfirmBox", msg);
                        if (msg.Result)
                            BgBrush = Brushes.CornflowerBlue;
                        else
                            BgBrush = null;
                    });
                return _CmdConfirm;
            }
        }

        private MyCommand _CmdCompute;

        /// <summary>
        /// Gets the CmdCompute.
        /// </summary>
        public MyCommand CmdCompute
        {
            get
            {
                if (_CmdCompute == null)
                    _CmdCompute = new MyCommand(
                    o =>
                    {
                        var msg = new ComputeMsgArgs(NumOne, NumTwo);
                        MsgManager.SendMsg("ShowComputeWindow", msg);
                        NumResult = msg.Result;
                    });
                return _CmdCompute;
            }
        }
    }
}
