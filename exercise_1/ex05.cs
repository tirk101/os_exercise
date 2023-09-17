using System.Threading;

namespace OS_Sync_Ex_05
{
    class Program
    {
        private static string x = "";
        private static bool exitflag = false;
        private static bool updateflag = false;
        private static object xLock = new object();
        

        static void ThWriteX(object i){
            while(exitflag == false){
                lock (xLock){
                    if (x != "exit" && updateflag == true){
                        Console.WriteLine("***Thread {0} : x = {1}***",i , x);
                        updateflag = false;
                    }
                }
            }
            Console.WriteLine("---Thread {0} exit---", i);
        }

        static void ThReadX(){
            string xx;
            while (exitflag == false){
                lock (xLock){
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit"){
                        exitflag = true;
                    }
                    x = xx;
                    updateflag = true;
                }
            }
        }

        static void Main(string[] args){
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);
            Thread C = new Thread(ThWriteX);
            Thread D = new Thread(ThWriteX);

            A.Start();
            B.Start(1); 
            C.Start(2);
            D.Start(3);
        }
    }
}
