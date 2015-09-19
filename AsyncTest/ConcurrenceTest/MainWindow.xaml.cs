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
        private int maxThread = 100;
        private List<TestTask> taskList;
        private int sleepTime = 50;
        private bool isWorking = false;
        private DateTime startTime;
        private int counter;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitTaskList()
        {
            taskList = new List<TestTask>();
            for (int i = 0; i < maxThread; i++)
            {
                taskList.Add(new TestTask(i));
            }
        }

        private void InitData()
        {
            isWorking = true;
            startTime = DateTime.Now;
            counter = 0;
        }

        private void ShowInfo(int id,int time)
        {
            var ts = DateTime.Now - startTime;
            counter++;
            var cps = counter / ts.TotalSeconds;
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() => InfoText.Text =
                $"[共运行{ts.TotalSeconds}秒，每秒接收 {cps} 次]  线程{id}收到{time}!\n"));
        }

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

        private void ThreadFunction(object o)
        {
            var t = o as TestTask;
            while (isWorking)
            {
                var time = t.ReceiveData();
                if (time != -1)
                {
                    ShowInfo(t.TaskID, time);
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(ThreadPoolFunction)
            { IsBackground = true }.Start();
        }

        private void ThreadPoolFunction()
        {
            while (isWorking)
            {
                foreach (var task in taskList)
                {
                    ThreadPool.QueueUserWorkItem((s =>
                    {
                        var time = task.ReceiveData();
                        if (time != -1)
                        {
                            ShowInfo(task.TaskID, time);
                        }
                    }));
                }
                Thread.Sleep(sleepTime);
            }
        }

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
                        if (time != -1)
                        {
                            ShowInfo(task.TaskID, time);
                        }
                    }),i);
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(TaskFunction)
            { IsBackground = true }.Start();
        }

        private void TaskFunction()
        {
            while(isWorking)
            {
                foreach (var task in taskList)
                {
                    //Task<int> t = new Task<int>(task.ReceiveData);
                    //t.ContinueWith(new Action<Task<int>>(r =>
                    //{
                    //    if (r.Result != -1)
                    //    {
                    //        ShowInfo(task.TaskID, r.Result);
                    //    }
                    //}));
                    //t.Start();
                    Task.Run(new Action(()=> {
                        var time = task.ReceiveData();
                        if (time != -1)
                        {
                            ShowInfo(task.TaskID, time);
                        }
                    }));
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            isWorking = false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(ParallelFunction2)
            { IsBackground = true }.Start();
        }

        private void ParallelFunction()
        {
            while(isWorking)
            {
                Parallel.ForEach(taskList, new Action<TestTask>(task =>
                {
                    var time = task.ReceiveData();
                    if (time != -1)
                    {
                        ShowInfo(task.TaskID, time);
                    }
                }));
                Thread.Sleep(sleepTime);
            }
        }

        private void ParallelFunction2()
        {
            while (isWorking)
            {
                Parallel.For(0, taskList.Count, new Action<int>(i => {
                    var task = taskList[i];
                    var time = task.ReceiveData();
                    if (time != -1)
                    {
                        ShowInfo(task.TaskID, time);
                    }
                }));
                Thread.Sleep(sleepTime);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            InitData();
            InitTaskList();

            new Thread(AwaitFunction)
            { IsBackground = true }.Start();
        }

        private async void AwaitFunction()
        {
            while(isWorking)
            {
                foreach (var task in taskList)
                {
                    var time = await task.ReceiveDataAsync();
                    if(time != -1)
                    {
                        ShowInfo(task.TaskID, time);
                    }
                    
                }
                Thread.Sleep(sleepTime);
            }
        }
    }

    class TestTask
    {
        Random rand;
        public int TaskID { get; private set; }

        public TestTask(int seed)
        {
            rand = new Random(seed);
            TaskID = seed;
        }
        public int ReceiveData()
        {
            var i = rand.Next(0, 1000);
            if (i < 990) return -1;
            return i;
        }

        public async Task<int> ReceiveDataAsync()
        {
            var task = new Task<int>(ReceiveData);
            task.Start();
            return await task;
        }
    }
}
