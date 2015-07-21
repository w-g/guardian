using Guardian.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian
{
    class Program
    {
        static void Main(String[] args)
        {
            while (true)
            {
                Console.Write("INPUT: ");
                string input = Console.ReadLine();

                Console.WriteLine("OUTPUT: E-{0}; D-{1}", Encrypt(input), Decrypt(input));
            }
        }

        static string Encrypt(string input)
        {
            // 奇数位置字符的ASKII位序加4，偶数位加1后进行Base64编码

            int index = 0;
            string output = string.Empty;

            foreach (char i in input)
            {
                if (index % 2 == 0)
                {
                    output += (char)(i + 1);
                }
                else
                {
                    output += (char)(i + 4);
                }

                index++;
            }

            return Convert.ToBase64String(Encoding.ASCII.GetBytes(output));
        }

        static string Decrypt(string input)
        {
            // Base64解码后奇数位字符ASKII位序各减4，偶数位减1

            try
            {
                int index = 0;
                string output = null;

                foreach (byte i in Convert.FromBase64String(input))
                {
                    if (index % 2 == 0)
                    {
                        output += (char)(i - 1);
                    }
                    else
                    {
                        output += (char)(i - 4);
                    }

                    index++;
                }

                return output;
            }
            catch(Exception ecp)
            {
                Console.WriteLine(ecp.Message);
                return string.Empty;
            }
        }
    }
}
