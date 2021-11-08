using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isu
{
    class Node
    {
        public int value;
        public int cost;
        public Node parent;
        public int depth;
        public List<Node> childs = new List<Node>();

        public Node( int value, int cost, Node parent = null, int depth = 0)
        {
            this.value = value;
            this.cost = cost;
            this.parent = parent;
            this.depth = depth;
        }
        /*
        public void AddChild(Node child)
        {
            childs.Add(child);
        }
        */
        public static Node MinCostNode(List<Node> nodes)
        {
            Node minNode = nodes[0];
            foreach (var node in nodes)
            {
                if (node.parent.parent is null)
                {
                    if (node.cost < minNode.cost)
                        minNode = node;
                }
                else
                {
                    if (node.cost < minNode.cost &&
                    node.value != node.parent.parent.value)
                        minNode = node;
                }
                
            }

            return minNode;
        }

        public static List<Node> MinCostNodes(List<Node> nodes)
        {
            Node minNode = nodes[0];
            List<Node> minNodes = new List<Node>();
            foreach (var node in nodes)
            {
                if (node.cost <= minNode.cost)
                {
                    minNode = node;
                    minNodes.Add(node);
                }
            }

            return minNodes;
        }
    }
}
