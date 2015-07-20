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
            //Memo memo = new Memo 
            //{
            //    Company = "阿里",
            //    Email = "w-zb@outlook.com",
            //    Password = "WZB@Alipay",
            //    Product = "支付宝",
            //    UserName = "w-zb@outlook.com"
            //};

            //memo.Save();

            Memo _memo = new Memo(1);

            File.WriteAllText("output.txt", _memo.Password);
        }
    }
}
