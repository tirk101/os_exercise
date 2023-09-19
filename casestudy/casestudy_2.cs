using System;
using System.Threading;

namespace OS_Problem_02
{
    class Thread_safe_buffer
    {
        static int[] TSBuffer = new int[10];
        static int Front = 0;
        static int Back = 0;
        static int Count = 0;
        static object qLock = new object();
        static int endflag = 0;

        static void EnQueue(int eq)
        {
            lock(qLock){
                TSBuffer[Back] = eq;
                Back++;
                Back %= 10;
                Count += 1;
                Monitor.Pulse(qLock);
            }
        }

        static int DeQueue()
        {
            int x = 0;
            lock(qLock){
                x = TSBuffer[Front];
                Front++;
                Front %= 10;
                Count -= 1;
                Monitor.Pulse(qLock);
            }
            return x;
        }

        static void th01(object start)
        {
            int i;
            for (i = (int)start; i < (int)start + 50; i++)
            {
                lock(qLock){
                    while(Count == 10) Monitor.Wait(qLock);
                    EnQueue(i);
                    Thread.Sleep(5);
                }
            }
            endflag++;
        }

        static void th02(object t)
        {
            int i;
            int j;

            for (i=0; i< 60; i++)
            {
                lock(qLock){
                    while(Count == 0){
                        if (endflag == 2) return;
                        Monitor.Wait(qLock);
                    } 
                    j = DeQueue();
                    Console.WriteLine(" j={0}, thread:{1}", j, t);
                    Thread.Sleep(100);
                }   
            }
        }
        static void Main(string[] args)
        {
            Thread t1 = new Thread(th01);
            Thread t11 = new Thread(th01);
            Thread t2 = new Thread(th02);
            Thread t21 = new Thread(th02);
            Thread t22 = new Thread(th02);

            t1.Start(1);
            t11.Start(100);
            t2.Start(1);
            t21.Start(2);
            t22.Start(3);
        }
    }
}
