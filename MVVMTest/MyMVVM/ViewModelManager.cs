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
        private static readonly List<ViewModelInfo> InstanceList = new List<ViewModelInfo>();
        private static readonly Dictionary<Type, Type> TypeList = new Dictionary<Type, Type>();

        public static void RegisterType<TView,TViewModel>()
        {
            var viewType = typeof (TView);
            if (TypeList.ContainsKey(viewType))
                return;
            var vmType = typeof (TViewModel);
            TypeList.Add(viewType, vmType);
        }

        public static object GetViewModel(object view)
        {
            if (view == null) return null;
            var viewType = view.GetType();
            if (!TypeList.ContainsKey(viewType))
                return null;
            var vmType = TypeList[viewType];
            return vmType.Assembly.CreateInstance(vmType.FullName);
        }

        public static object GetViewModel<T>(object view)
        {
            if (view == null) return null;
            var viewType = view.GetType();
            var vmType = typeof (T);
            if (!TypeList.ContainsKey(viewType))
            {
                TypeList.Add(viewType, vmType);
            }
            return vmType.Assembly.CreateInstance(vmType.FullName);
        }
    }
}
