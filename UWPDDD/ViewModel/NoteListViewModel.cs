using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyxMVVM;
using System.Collections.ObjectModel;
using Domain.Model;

namespace ViewModel
{
    public class NoteListViewModel:ObserveObject
    {
        private ObservableCollection<ANote> _NoteList;
        public ObservableCollection<ANote> NoteList
        {
            get { return _NoteList; }
            set
            {
                if (_NoteList != value)
                {
                    _NoteList = value;
                    RaisePropertyChanged("NoteList");
                }
            }
        }

        public NoteListViewModel()
        {
            NoteList = new ObservableCollection<ANote>
            {
                new ANote
                {
                    AddDateTime = DateTime.Now.ToString(),
                    EditDateTime = DateTime.Now.ToString(),
                    Title = "测试数据，标题一定要长才行!!",
                    Category = new ACategory {Category="默认分组" }
                },
                new ANote
                {
                    AddDateTime = DateTime.Now.ToString(),
                    EditDateTime = DateTime.Now.ToString(),
                    Title = "This is test data!",
                    Category = new ACategory {Category="我的笔记" }
                }
            };
        }
    }
}
