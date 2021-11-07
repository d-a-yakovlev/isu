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
        public Node( int value, int cost, Node parent = null, int depth = 0)
        {
            this.value = value;
            this.cost = cost;
            this.parent = parent;
            this.depth = depth;
        }
    }
}
