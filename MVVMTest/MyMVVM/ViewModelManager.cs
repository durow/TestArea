using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;

namespace MyMVVM
{
    public static class ViewModelManager
    {
        private static List<ViewModelInfo> _viewModelInfoList = new List<ViewModelInfo>();

        public static void Register<TView,TViewModel,TMsgRegister>(string token="")
        {
            var vmInfo = new ViewModelInfo(
                typeof(TView),
                typeof(TViewModel),
                typeof(TMsgRegister),
                token);
            _viewModelInfoList.Add(vmInfo);
        }

        public static void Register<TView,TViewModel>(string token="")
        {
            var vmInfo = new ViewModelInfo(
                typeof(TView),
                typeof(TViewModel),
                token:token);
            _viewModelInfoList.Add(vmInfo);
        }

        public static object GetViewModel<TView>(string token="")
        {
            try
            {
                var vmType = GetViewModelInfo(typeof(TView), token).ViewModelType;
                return vmType.Assembly.CreateInstance(vmType.FullName);
            }
            catch
            {
                return null;
            }
        }

        public static void SetViewModel(FrameworkElement fe, bool isGlobalMsg = false, string token="")
        {
            var vmInfo = GetViewModelInfo(fe.GetType(), token);
            if (vmInfo == null) return;
            var vm = vmInfo.GetViewModelInstance();
            //set ViewModel's UIDispatcher
            vm.UIDispatcher = fe.Dispatcher;
            //set View's DataContext
            fe.DataContext = vm;
            //register View's message
            var msgRegister = vmInfo.GetMsgRegisterInstance();
            if (msgRegister == null) return;

            msgRegister.RegInstance = fe;
            if (isGlobalMsg)
            {
                var win = fe as Window;
                if (win == null)
                {
                    throw new Exception("only can set a Window's message as global!");
                }
                vm.MsgManager = MessageManager.Default;
                win.Closed += MessageManager.Default.WindowClose;
            }
            else
            {
                vm.MsgManager = new MessageManager();
            }
            msgRegister.MsgManager = vm.MsgManager;
            msgRegister.Register();
        }

        private static ViewModelInfo GetViewModelInfo(Type viewType,string token="")
        {
            try
            {
                return _viewModelInfoList
                    .Where(p => p.ViewType == viewType && p.Token == token)
                    .First();
            }
            catch
            {
                return null;
            }
        }

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
