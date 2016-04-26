using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    [Serializable()]
    public class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Boolean IsSamePosition(int x, int y)
        {
            return this.x == x && this.y == y;
        }

        public Boolean IsSamePosition(Position compare)
        {
            return this.x == compare.x && this.y == compare.y;
        }

    }
}
