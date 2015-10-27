using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyxMVVM;
using System.Collections.ObjectModel;
using Domain.Model;
using Domain.Repository;

namespace ViewModel
{
    public class NoteListViewModel:ViewModelBase
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
            
        }

        public override void InitTestData()
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
                },
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
                },
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
                },
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

        public override void InitRealData()
        {
            var notes = Repositories.ANoteRepository.GetAll();
            if (notes == null) return;
            NoteList = new ObservableCollection<ANote>(notes);
        }
    }
}
