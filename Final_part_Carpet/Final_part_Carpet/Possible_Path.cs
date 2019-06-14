using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_part_Carpet
{
    class Possible_Path : IComparable<Possible_Path>
    {
        

        public Possible_Path(Possible_Path p,int dist ,int poss)
        {
            this.Distance = p.Distance;
            // possible exception ^
            this.ll = new List<int>();
            for(int i=0;i<p.ll.Count;i++)
            {
                this.ll.Add(p.ll[i]);
            }

            this.ll.Add(dist);
            this.ll.Add(poss);
            this.Distance += dist;
            updist = dist;
        }

        

        public Possible_Path(int start)
        {
            this.Distance = 0;
            updist = 0;
            this.ll = new List<int>();
            ll.Add(start);
        }

        public List<int> ll;

        int Distance;

        int updist;

        public int CompareTo(Possible_Path other)
        {
            return this.Distance.CompareTo(other.Distance);
        }


        public override string ToString()
        {
            String x ="";
            for(int i=0; i< ll.Count-1; i+=2)
            {
                x += $" Node {ll[i]} --> Node {ll[i + 2]} - dist:{ll[i+1]}";
                x+= '\n';
            }

            x += "\n total distance from nearest factory is: " + Distance;
            return x;
        }
    }
}
