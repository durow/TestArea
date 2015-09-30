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
        public static void RegisterViewModel(FrameworkElement view, ViewModelBase viewmodel, IMessageRegister msgRegister=null)
        {
            if (view == null || viewmodel==null) return;
            //设定数据环境
            view.DataContext = viewmodel;
            //设置ViewModel的Dispatcher
            viewmodel.UIDispatcher = view.Dispatcher;
            
            //无需注册消息则直接返回
            if (msgRegister == null) return;
            if (msgRegister.RegInstance == null)
                msgRegister.RegInstance = view;

            viewmodel.MsgManager = msgRegister.MsgManager;

            var win = view as Window;
            if (win != null)
                win.Closed += msgRegister.MsgManager.WindowClose;
            //注册消息
            msgRegister.Register();
        }
    }
}
