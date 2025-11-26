using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorizor
{
    internal class Input
    {
        public static int GetMenuOption(int optionCount)
        {
            return GetSpecificIntFromUser(1, optionCount);
        }

        private static String GetStringFromUser()
        {
            return Console.ReadLine();
        }

        private static int GetIntFromUser()
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

        private static int GetSpecificIntFromUser(int min, int max)
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
