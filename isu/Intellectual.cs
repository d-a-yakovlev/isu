using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isu
{
    public class Intellectual
    {
        static Dictionary<int, int[]> Coord = new Dictionary<int, int[]>() {
            { 1, new int[]{ 0, 0} },
            { 2, new int[]{ 0, 1} },
            { 3, new int[]{ 0, 2} },
            { 4, new int[]{ 1, 0} },
            { 5, new int[]{ 1, 1} },
            { 6, new int[]{ 1, 2} },
            { 7, new int[]{ 2, 0} },
            { 8, new int[]{ 2, 1} },
            { 9, new int[]{ 2, 2} },
        };

        public static int Heuristic(Dictionary<int, int> state)
        {
            int result = 0;
            foreach (var pos in state.Keys)
                result += Manhattan(pos, state[pos]);
            return result;
        }

        static int Manhattan(int a, int b)
        {
            if ( a != b)
            {
                int[] coordA = Coord[a];
                int[] coordB = Coord[b];
                return Math.Abs(coordA[0] - coordB[0]) + Math.Abs(coordA[1] - coordB[1]);
            }
            return 0;
        }
    }
}
