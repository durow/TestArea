using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMVVM
{
    public class ViewModelInfo
    {
        public Type ViewType { get; private set; }
        public Type ViewModelType { get; private set; }
        public Type MsgRegisterType { get; private set; }
        public string Token { get; private set; }

        public ViewModelInfo(Type view,Type viewModel,Type msgRegister=null,string token="")
        {
            ViewType = view;
            ViewModelType = viewModel;
            MsgRegisterType = msgRegister;
            Token = token;
        }

        public ViewModelBase GetViewModelInstance()
        {
            if (ViewModelType == null) return null;
            return ViewModelType
                .Assembly
                .CreateInstance(ViewModelType.FullName)
                as ViewModelBase;
        }

        public IMessageRegister GetMsgRegisterInstance()
        {
            if (MsgRegisterType == null) return null;
            return MsgRegisterType
                .Assembly
                .CreateInstance(MsgRegisterType.FullName)
                as IMessageRegister;
        }
    }
}
