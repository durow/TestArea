using System.ComponentModel;

namespace MyMVVM
{
    public class NotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性发生改变时调用该方法发出通知
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void SetAndNotiryIfChanged<T>(string propertyName, ref T oldValue, T newValue)
        {
            if (oldValue.Equals(newValue)) return;
            oldValue = newValue;
            RaisePropertyChanged(propertyName);
        }
    }
}
