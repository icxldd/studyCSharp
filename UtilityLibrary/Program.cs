using DataStructureBasic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UtilityLibraryCore.Byte;
using UtilityLibraryCore.Time;
using Newtonsoft.Json;
namespace UtilityLibrary
{
    class Animal
    {
        public string Species { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        public delegate void BoilerLogHandler(string status);
        public static event BoilerLogHandler BoilerEventLog;
        public static Action<string> aaa;
        public static Func<string, string> aaaa;



        public static byte Set_Bit(byte data, int index, bool flag)
        {
            if (index > 8 || index < 1)

                throw new ArgumentOutOfRangeException();

            int v = index < 2 ? index : (2 << (index - 2));

            return flag ? (byte)(data | v) : (byte)(data & ~v);

        }
        static void Main(string[] args)
        {
            TreeBaseContext tree = new TreeBaseContext();
            var rootNode = tree.AddNewNode("icxl公司");

            var childNode1 = tree.AddChildNode(rootNode.Id, "分公司1");
            var childNode2 = tree.AddChildNode(rootNode.Id, "分公司2");
            var childNode3 = tree.AddChildNode(rootNode.Id, "分公司3");

            var childCNode1 = tree.AddChildNode(childNode1.Id, "分公司1-人事部");
            var childCNode2 = tree.AddChildNode(childNode2.Id, "分公司2-人事部");
            var childCNode3 = tree.AddChildNode(childNode3.Id, "分公司3-人事部");

            var childCCNode1 = tree.AddChildNode(childCNode1.Id, "人事部1-A成员");
            var childCCNode2 = tree.AddChildNode(childCNode2.Id, "人事部2-A成员");
            var childCCNode3 = tree.AddChildNode(childCNode3.Id, "人事部3-A成员");

            //tree.Delete(4);

            tree.Show();
            //Console.WriteLine("请输入需要添加的根节点名称");

            //string rootName = Console.ReadLine();
            //var rootNode = tree.AddNewNode(rootName);
            //Console.WriteLine($"添加成功，节点ID为{rootNode.Id}");

            //while (true)
            //{
            //    Console.WriteLine("请输入是要插入还是显示0：显示；1：插入");

            //    if (Convert.ToInt32(Console.ReadLine()) == 0)
            //    {
            //        Console.WriteLine("请输入需要显示什么0：上层；1：下层");

            //        int val = Convert.ToInt32(Console.ReadLine());
            //        if (val == 0)
            //        {
            //            Console.WriteLine("请输入节点ID");
            //            int Id = Convert.ToInt32(Console.ReadLine());
            //            Console.WriteLine(JsonConvert.SerializeObject(tree.GetAncestorNode(Id)));

            //        }
            //        else if (val == 1)
            //        {

            //            Console.WriteLine("请输入节点ID");
            //            int Id = Convert.ToInt32(Console.ReadLine());
            //            Console.WriteLine(JsonConvert.SerializeObject(tree.GetDescendantNode(Id)));
            //        }

            //    }
            //    else
            //    {

            //        Console.WriteLine("请输入需要添加的子节点名称");
            //        string childName = Console.ReadLine();
            //        Console.WriteLine("请输入父节点ID");
            //        string parentId = Console.ReadLine();
            //        var childNode = tree.AddChildNode(Convert.ToInt32(parentId), childName);
            //        tree.Show();
            //    }
            //}




        }

        static protected void OnBoilerEventLog(string message)
        {
            Console.WriteLine("OnBoilerEventLog");
        }


        static protected void OnBoilerEventLog2(string message)
        {
            Console.WriteLine("OnBoilerEventLog2");
        }


        static protected string OnBoilerEventLog33(string message)
        {
            Console.WriteLine("OnBoilerEventLog");
            return "";
        }


        static protected string OnBoilerEventLog44(string message)
        {
            Console.WriteLine("OnBoilerEventLog2");
            return "";
        }

        static void Print(params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }
        }
    }
}
