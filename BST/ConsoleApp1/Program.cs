using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            JObject json = JObject.Parse("{ \"type\":\"BinaryExpression\", \"operator\":\" * \", \"left\":{ \"type\":\"BinaryExpression\", \"operator\":\" + \", \"left\":{ \"type\":\"BinaryExpression\", \"operator\":\" + \", \"left\":{ \"type\":\"Literal\", \"value\":1, \"raw\":\"1\" }, \"right\":{ \"type\":\"Literal\", \"value\":1, \"raw\":\"1\" } }, \"right\":{ \"type\":\"Literal\", \"value\":3, \"raw\":\"3\" } }, \"right\":{ \"type\":\"Literal\", \"value\":2, \"raw\":\"2\" } }");
            // IEnumerator<KeyValuePair<string, JToken>> iterator = jObject.GetEnumerator();
            Node list = new Node();
            list.InsertToBST(json, null);
            Console.WriteLine("==========Result===========");
            Console.WriteLine(list.eval(list.getRootNode()));

            Console.ReadKey();

             
        }
    }
}
