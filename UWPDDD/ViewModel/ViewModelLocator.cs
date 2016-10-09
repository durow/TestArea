using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using AyxMVVM;

namespace ViewModel
{
    public class ViewModelLocator:VMLocatorBase
    {
        private NoteListViewModel _noteList;

        public NoteListViewModel NoteList
        {
            get
            {
                if (_noteList == null)
                {
                    _noteList = GetViewModel<NoteListViewModel>();
                }
                return _noteList;
            }
        }

        private NoteInfoViewModel _noteInfo;

        public NoteInfoViewModel NoteInfo
        {
            get
            {
                if (_noteInfo == null)
                {
                    _noteInfo = GetViewModel<NoteInfoViewModel>();
                }
                return _noteInfo;
            }
        }


    }
}
