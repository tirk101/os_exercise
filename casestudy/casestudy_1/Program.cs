using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Problem01
{
    class Program
    {
        static byte[] Data_Global = new byte[1000000000];
        static long Sum_Global = 0;
        static int All_Thread = Environment.ProcessorCount;

        static int ReadData()
        {
            int returnData = 0;
            FileStream fs = new FileStream("Problem01.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            try 
            {
                Data_Global = (byte[]) bf.Deserialize(fs);
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Read Failed:" + se.Message);
                returnData = 1;
            }
            finally
            {
                fs.Close();
            }

            return returnData;
        }
        static void sum(object start)
        {
            long Sum_Local = 0;
            for(int i=(int)start; i<1000000000; i+=All_Thread){
                byte temp = Data_Global[i];
                Data_Global[i] = 0;
                if (temp != 1 && temp != 0){
                    if ((temp & 1) == 0)    Sum_Local -= temp;
                    else if (temp % 3 == 0) Sum_Local += (temp << 1);
                    else if (temp % 5 == 0) Sum_Local += (temp >> 1);
                    else if (temp % 7 == 0) Sum_Local += (temp / 3);
                }  
            }
            Sum_Global += Sum_Local;
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int i, y;

            /* Read data from file */
            Console.Write("Data read...");
            y = ReadData();
            if (y == 0)
            {
                Console.WriteLine("Complete.");
            }
            else
            {
                Console.WriteLine("Read Failed!");
            }

            /* Start */
            Console.Write("\n\nWorking...");
            Thread[] th = new Thread[All_Thread];
            
            sw.Start();
            for (i = 0; i < All_Thread; i++){
                th[i] = new Thread(sum);
                th[i].Start(i);
            }
            for (i = 0; i < All_Thread; i++) th[i].Join();
            sw.Stop();
            Console.WriteLine("Done.");

            /* Result */
            Console.WriteLine("Summation result: {0}", Sum_Global);
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
