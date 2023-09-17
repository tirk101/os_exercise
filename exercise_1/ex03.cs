using System;
using System.Diagnostics;
using System.Threading;

namespace OS_Sync_Ex_03
{
    class Program
    {
        private static int sum = 0;
        private static object xLock = new object();

        static void plus(){
            int i;
            lock (_Lock){
                for (i = 1; i < 1000001; i++){
                    sum += i;
                }
            } 
        }

        static void minus(){
            int i;
            lock (_Lock){
                for (i = 0; i < 1000000; i++){
                    sum -= i;
                }
            } 
        }

        static void Main(string[] args){
            string xx;
            Console.Write("Input: ");
            xx = Console.ReadLine();
            Console.WriteLine("X = {0}", xx);
        }
    }
}
