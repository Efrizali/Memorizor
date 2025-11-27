using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorizor
{
    internal class CMDInput
    {

        public static String? GetStringFromUser()
        {
            return Console.ReadLine();
        }

        public static int GetIntFromUser()
        {
            while (true)
            {
                Console.Write("Int: ");
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    return input;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        public static int GetSpecificIntFromUser(int min, int max)
        { 
            while (true)
            {
                Console.Write("Int: ");
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    if (input < min | input > max) continue;
                    return input;
                } catch (Exception e)
                {
                    continue;
                }
            }
            
        }
    }
}
