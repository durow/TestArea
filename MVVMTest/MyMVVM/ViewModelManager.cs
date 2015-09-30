using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;

namespace MyMVVM
{
    public class ViewModelManager
    {
        private IMessageRegister _messageRegister;
        public IMessageRegister MessageRegister
        {
            private get { return _messageRegister; } 
            set
            {
                if (_messageRegister != value)
                {
                    _messageRegister = value;
                    MessageRegister.Register();
                }
            }
        }
    }
}
