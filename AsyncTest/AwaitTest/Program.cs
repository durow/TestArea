using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程测试开始..");
            AsyncMethod();
            Thread.Sleep(1000);
            Console.WriteLine("主线程测试结束..");
            Console.ReadLine();
        }

        static void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");
            MyMethod();
            Console.WriteLine("异步代码执行完毕");
        }

        static async void MyMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000); //模拟耗时操作
                Console.WriteLine("异步执行完毕" + i.ToString() + "..");
            }
        }
    }
}
