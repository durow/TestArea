using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace MyMVVM
{
    public class ViewModelBase : NotifyObject
    {
        private IMessageManager _msgManager;

        public IMessageManager MsgManager
        {
            get
            {
                if (_msgManager == null)
                    _msgManager = MessageManager.Default;
                return _msgManager;
            }
            set { _msgManager = value; }
        }

        private Dispatcher _UIDispatcher;

        public Dispatcher UIDispatcher
        {
            get
            {
                if (_UIDispatcher == null)
                    _UIDispatcher = DispatcherHelper.UIDispatcher;
                return _UIDispatcher;
            }
            set { _UIDispatcher = value; }
        }

    }
}
