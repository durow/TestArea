using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyxMVVM;
using Domain.Model;
using AyxMVVM.Command;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace ViewModel
{
    public class NoteInfoViewModel:ViewModelBase
    {
        private ANote _Note;

        public ANote Note
        {
            get { return _Note; }
            set
            {
                if (_Note != value)
                {
                    value.UpdateContent();
                    _Note = value;
                    RaisePropertyChanged("Note");
                }
            }
        }

        public NoteInfoViewModel()
        {
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame.GoBack();
            }
            catch { }
        }

        private AyxCommand _CmdBack;

        /// <summary>
        /// Gets the CmdBack.
        /// </summary>
        public AyxCommand CmdBack
        {
            get
            {
                if (_CmdBack == null)
                    _CmdBack = new AyxCommand(
                    () =>
                    {
                        new MessageDialog("test").ShowAsync();
                    });
                return _CmdBack;
            }
        }

        public override void InitTestData()
        {
            Note = new ANote
            {
                AddDateTime = DateTime.Now.ToString(),
                EditDateTime = DateTime.Now.ToString(),
                Title = "测试信息这是测试信息！",
                Content = @"新华网北京10月26日电 联合国教科文组织第九届青年论坛26日在巴黎开幕。国家主席习近平发去贺词，代表中国政府和人民，对论坛的举行表示热烈的祝贺。贺词全文如下：
习近平主席在联合国教科文组织第九届青年论坛开幕式上的贺词
亲爱的青年朋友们：
值此第九届青年论坛开幕暨联合国教科文组织成立70周年之际，我谨代表中国政府和人民，并以我个人的名义，对论坛的举行表示热烈的祝贺！本届论坛以“推动可持续发展，塑造全球公民”为主题，对促进可持续发展、开展文明对话具有重要意义。前不久，联合国可持续发展峰会通过了2030年可持续发展议程。落实好项议程确定的目标，建立一个公平、包容、可持续的地球，是包括全球青年在内的每个人都要重视和担当的责任。",
                Category = new ACategory { Category = "默认分类" },
            };
        }

        public override void InitRealData()
        {
            InitTestData();
        }
    }
}
