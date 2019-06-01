using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpet
{
    class Graph_Color
    {
        public int size { get; private set; }
        public Graph_Color(int s)
        {
            size = s;
        }

        public bool[,] graph;

        public Dictionary<int, int> Cols = new Dictionary<int, int>();

        public void Calculate()
        {
            Cols[0] = 0;
             SortedSet<int> tmp;

            for(int i=1; i < size ; i++)
            {
                tmp = new SortedSet<int>();
                for(int j=0; j < i ; j++)
                {
                   if(graph[i,j])
                   {
                        tmp.Add(Cols[j]);
                   }
                }
                int u = 0;
                foreach (int x in tmp)
                {
                    
                    if(x != u)
                    {
                        break;
                    }
                    u++;
                }
                Cols[i] = u;
            }
        }

        public int Show_max_Color()
        {
            SortedSet<int> a = new SortedSet<int>();
            foreach(var item in Cols.Values)
            {
                a.Add(item);
            }

            return a.Last();
        }
    }
}
