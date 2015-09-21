/*****************************************************************
 * 功能:测试并发效率
 * 作者:durow
 * 时间:2015.09.20
 *****************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ConcurrenceTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int maxThread = 500;
        private List<TestTask> taskList;
        private int sleepTime = 50;
        private bool isWorking = false;
        private DateTime startTime;
        private int counter;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化对象列表
        /// </summary>
        private void InitTaskList()
        {
            taskList = new List<TestTask>();
            for (int i = 0; i < maxThread; i++)
            {
                taskList.Add(new TestTask(i));
            }
        }

        /// <summary>
        /// 初始化测试开始前的数据环境
        /// </summary>
        private void InitData()
        {
            isWorking = true;
            startTime = DateTime.Now;
            counter = 0;
        }

        /// <summary>
        /// 显示测试信息
        /// </summary>
        /// <param name="id">线程ID</param>
        /// <param name="time">随机出的时间</param>
        private void ShowInfo(int id,int time)
        {
            var ts = DateTime.Now - startTime;
            var cps = counter / ts.TotalSeconds;
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() => InfoText.Text =
                $"[共运行{ts.TotalSeconds}秒，每秒接收 {cps} 次]  线程{id}收到{time}!\n"));
        }

        #region 保持线程的并发

        //保持线程的并发按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitData();
            for (int i = 0; i < maxThread; i++)
            {
                var task = new TestTask(i);
                new Thread(ThreadFunction)
                { IsBackground = true }
                .Start(task);
            }
        }

        //线程函数
        private void ThreadFunction(object o)
        {
            var t = o as TestTask;
            while (isWorking)
            {
                var time = t.ReceiveData();
                counter++;
                if (time != -1)
                {
                    ShowInfo(t.TaskID, time);
                }
                Thread.Sleep(sleepTime);
            }
        }

        #endregion

        #region 使用线程池的轮询并发

        //线程池并发按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(ThreadPoolFunction)
            { IsBackground = true }.Start();
        }

        /// <summary>
        /// 用foreach轮询
        /// </summary>
        private void ThreadPoolFunction()
        {
            while (isWorking)
            {
                foreach (var task in taskList)
                {
                    ThreadPool.QueueUserWorkItem((s =>
                    {
                        var time = task.ReceiveData();
                        counter++;
                        if (time != -1)
                        {
                            ShowInfo(task.TaskID, time);
                        }
                    }));
                }
                Thread.Sleep(sleepTime);
            }
        }

        /// <summary>
        /// 用for轮询
        /// </summary>
        private void ThreadPoolFunction2()
        {
            while (isWorking)
            {
                for (int i = 0; i < taskList.Count; i++)
                {
                    ThreadPool.QueueUserWorkItem((s =>
                    {
                        int index = (int)s;
                        var task = taskList[index];
                        var time = task.ReceiveData();
                        counter++;
                        if (time != -1)
                        {
                            ShowInfo(task.TaskID, time);
                        }
                    }),i);
                }
                Thread.Sleep(sleepTime);
            }
        }

        #endregion

        #region 使用Task轮询的并发

        //Task轮询并发按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(TaskFunction)
            { IsBackground = true }.Start();
        }

        /// <summary>
        /// Task轮询
        /// </summary>
        private void TaskFunction()
        {
            while(isWorking)
            {
                foreach (var task in taskList)
                {
                    Task.Run(new Action(()=> {
                        var time = task.ReceiveData();
                        counter++;
                        if (time != -1)
                        {
                            ShowInfo(task.TaskID, time);
                        }
                    }));
                }
                Thread.Sleep(sleepTime);
            }
        }

        #endregion

        #region Parallel轮询并发

        //Parallel并发按钮
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(ParallelFunction2)
            { IsBackground = true }.Start();
        }

        /// <summary>
        /// 使用Parallel.ForEach()并发
        /// </summary>
        private void ParallelFunction()
        {
            while(isWorking)
            {
                Parallel.ForEach(taskList, new Action<TestTask>(task =>
                {
                    var time = task.ReceiveData();
                    counter++;
                    if (time != -1)
                    {
                        ShowInfo(task.TaskID, time);
                    }
                }));
                Thread.Sleep(sleepTime);
            }
        }

        /// <summary>
        /// 使用Parallel.For()并发
        /// </summary>
        private void ParallelFunction2()
        {
            while (isWorking)
            {
                Parallel.For(0, taskList.Count, new Action<int>(i => {
                    var task = taskList[i];
                    var time = task.ReceiveData();
                    counter++;
                    if (time != -1)
                    {
                        ShowInfo(task.TaskID, time);
                    }
                }));
                Thread.Sleep(sleepTime);
            }
        }

        #endregion

        #region await轮询并发

        //await轮询并发按钮
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(AwaitFunction)
            { IsBackground = true }.Start();
        }

        /// <summary>
        /// await轮询
        /// </summary>
        private async void AwaitFunction()
        {
            while(isWorking)
            {
                foreach (var task in taskList)
                {
                    var t = new Task<int>(task.ReceiveData);
                    t.Start();
                    var time = await t;
                    counter++;
                    if (time != -1)
                    {
                        ShowInfo(task.TaskID, time);
                    }
                    
                }
                Thread.Sleep(sleepTime);
            }
        }

        #endregion

        //停止按钮
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            isWorking = false;
        }
    }

    #region 执行任务的对象

    class TestTask
    {
        Random rand;
        public int TaskID { get; private set; }

        public TestTask(int seed)
        {
            rand = new Random(seed);
            TaskID = seed;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns>接收到的数据，-1表示未接收</returns>
        public int ReceiveData()
        {
            var i = rand.Next(0, 1000);
            if (i < 990) return -1;
            return i;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns>-1表示未接收到</returns>
        public async Task<int> ReceiveDataAsync()
        {
            var task = new Task<int>(ReceiveData);
            task.Start();
            return await task;
        }
    }

    #endregion
}
