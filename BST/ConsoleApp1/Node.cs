using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Node
    {

        string data;
        Node next;
        Node left;
        Node right;
        Node root;

        public Node()
        {
            this.root = null;
            this.left = null;
            this.right = null;
        }

        Node(string data)
        {
            this.data = data;
            this.next = null;
        }

        bool isOperator(char c)
        {
            if (c == '+' || c == '-'
                    || c == '*' || c == '/'
                    || c == '^')
            {
                return true;
            }
            return false;
        }

        public Node getRootNode ()
        {
            return this.root;
        }

        public int eval(Node node)
        {
            if (root == null)
                return 0;

            if ((node.left == null) && (node.right == null))
                return Int32.Parse(node.data);

            int lval = eval(node.right);
            int rval = eval(node.right);

            if (node.data.Equals("+"))
                return lval + rval;

            if (node.data.Equals("-"))
                return lval - rval;

            if (node.data.Equals("*"))
                return lval * rval;

            if (node.data.Equals("/"))
                return lval / rval;

            return 0;
        }

        public void InsertToBST(JObject jObject, Node current)
        {

            //   JObject jObject = JObject.Parse(json);
            // IEnumerator<KeyValuePair<string, JToken>> iterator = jObject.GetEnumerator();

            Node parent = current;

            while (jObject.Count > 0)
            {
                foreach (KeyValuePair<string, JToken> item in jObject)
                {
                    int iKey = 0;
                    string key = item.Key;
                    JToken jToken = item.Value;
                    string value = jToken.ToString();

                    if (key.Equals("Literal") || key.Equals("operator"))
                    {
                        Node newNode = new Node(value);
                        if (root == null)
                            root = newNode;
                    }


                    if (key.Equals("left")) {
                        if(current != null)
                          current = current.left;

                        if (key.Equals("Literal") || key.Equals("operator"))
                        {
                            Node newNode = new Node(value);
                            createBranch(current, newNode, "left");
                        }
                        InsertToBST(jToken.ToObject<JObject>(), current);
                    }

                    if (key.Equals("right")) {
                        if (current != null)
                            current = current.right;
                        if (key.Equals("Literal") || key.Equals("operator"))
                        {
                            Node newNode = new Node(value);
                            createBranch(current, newNode, "left");
                        }
                        InsertToBST(jToken.ToObject<JObject>(), current);
                    }

                }
            } 

        }

        public void Postorder(Node Root)
        {

            if (Root != null)
            {
                Postorder(Root.left);
                Postorder(Root.right);
                Console.Write(Root.data + "-->");
            }
        }

        Node createBranch(Node current, Node newNode, string pos)
        {
            Node parent = current;

            if (pos.Equals("left"))
            {
               
                if (current == null)
                {
                    parent.left = newNode;
                    current = current.left;
                }
            } else
            {
                if (current == null)
                {
                    parent.right = newNode;
                    current = current.right;
                }
            }

            return current;
        }


        public void Traverse()
        {
            if (root == null)
                System.Console.WriteLine("list empty");
            else
            {
                Node node = root;
                while (node != null)
                {
                    System.Console.WriteLine(node.data);
                    node = node.next;
                }
            }
        }
    }
}
