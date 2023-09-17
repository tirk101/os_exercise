using System.Threading;

namespace OS_Sync_Ex_04
{
    class Program
    {
        private static string x = "";
        private static bool exitflag = false;
        private static bool updateflag = false;
        private static object xLock = new object();

        static void ThWriteX(){
            while(exitflag == false){
                lock (xLock){
                    if (updateflag == true){
                        Console.WriteLine("X = {0}", x);
                        updateflag = false;
                    }
                }
            }
            Console.WriteLine("Thread 1 exit");
        }

        static void ThReadX(){
            string xx;
            while (exitflag == false){
                lock (xLock){
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit"){
                        exitflag = true;
                    }else{
                        x = xx;
                        updateflag = true;
                    }
                }
            }
        }

        static void Main(string[] args){
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);

            A.Start();
            B.Start(); 
        }
    }
}
