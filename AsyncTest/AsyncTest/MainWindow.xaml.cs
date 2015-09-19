using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AsyncTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        int maxThread = 100;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 随机休眠模拟任务执行耗时
        /// </summary>
        /// <returns>任务消耗的时间(毫秒)</returns>
        int TestTask()
        {
            var time = rand.Next(100, 500);
            Thread.Sleep(time);
            return time; 
        }

        /// <summary>
        /// 支持await的任务
        /// </summary>
        /// <returns>任务消耗时间(毫秒)</returns>
        async Task<int> TestTaskAsync()
        {
            var task = new Task<int>(TestTask);
            task.Start();
            return await task;
        }

        /// <summary>
        /// 测试异步性能
        /// </summary>
        /// <param name="action">要测试的异步行为</param>
        /// <returns>耗费的时间</returns>
        private double AsyncTest(Action action)
        {
            var start = DateTime.Now;
            for (int i = 0; i < maxThread; i++)
            {
                action();
            }
            var ts = DateTime.Now - start;
            return ts.TotalMilliseconds;
        }

        /// <summary>
        /// 输出任务执行信息
        /// </summary>
        /// <param name="sender">按下的按钮</param>
        /// <param name="time">任务执行时间</param>
        void OutputInfo(Button sender,int time)
        {
            OutInfo.Text += $"\"{sender.Content}\"任务用时{time}毫秒！\n";
        }

        /// <summary>
        /// 输出测试结果
        /// </summary>
        /// <param name="sender">按下的按钮</param>
        /// <param name="time">测试耗费时间</param>
        void ShowTestResult(object sender, double time)
        {
            var bt = sender as Button;
            if (bt == null) return;

            OutInfo.Text += $"{bt.Content}测试：启动{maxThread}个任务，共耗时：{time}毫秒！\n";
        }

        //清除输出的信息
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OutInfo.Clear();
        }

        //线程异步按钮
        private void ThreadButton_Click(object sender, RoutedEventArgs e)
        {
            new Thread(o =>
            {
                var time = TestTask();
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new Action<Button, int>(OutputInfo), sender as Button, time);
            })
            { IsBackground = true }
            .Start();
        }

        //线程池异步按钮
        private void ThreadPoolButton_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                var time = TestTask();
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new Action<Button, int>(OutputInfo), sender as Button, time);
            });
        }

        //Task异步按钮
        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            var t = new Task<int>(TestTask);
            t.ContinueWith(p =>
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new Action<Button, int>(OutputInfo), sender as Button, p.Result);
            });
            t.Start();
        }

        //await异步按钮
        private async void AwaitButton_Click(object sender, RoutedEventArgs e)
        {
            Task<int> t = new Task<int>(TestTask);
            t.Start();
            var time = await t;
            OutputInfo(sender as Button, time);
            //var time = await TestTaskAsync();
            //OutputInfo(sender as Button, time);
        }

        //线程异步性能测试
        private void ThreadTest_Click(object sender, RoutedEventArgs e)
        {
            var time = AsyncTest(new Action(() => ThreadButton_Click(ThreadButton, null)));
            ShowTestResult(sender, time);
        }

        //线程池异步性能测试
        private void ThreadPoolTest_Click(object sender, RoutedEventArgs e)
        {
            var time = AsyncTest(new Action(() => ThreadPoolButton_Click(ThreadPoolButton, null)));
            ShowTestResult(sender, time);
        }

        //Task异步性能测试
        private void TaskTest_Click(object sender, RoutedEventArgs e)
        {
            var time = AsyncTest(new Action(()=> TaskButton_Click(TaskButton, null)));
            ShowTestResult(sender, time);
        }

        //await异步性能测试
        private void AwaitTest_Click(object sender, RoutedEventArgs e)
        {
            var time = AsyncTest(new Action(() => AwaitButton_Click(AwaitButton,null)));
            ShowTestResult(sender, time);
        }
    }
}
