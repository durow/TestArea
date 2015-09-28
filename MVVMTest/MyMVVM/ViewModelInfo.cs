using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MyMVVM
{
    public class ViewModelInfo
    {
        public object View { get; set; }
        public object ViewModel { get; set; }
        public Type ViewType { get; set; }
        public Type ViewModelType { get; set; }
    }

}
