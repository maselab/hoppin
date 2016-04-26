using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    [Serializable()]
    public class PlayerData
    {
        private int shoes;
        private int score;
        public Position position;

        public PlayerData(int pointX, int pointY)
        {
            this.shoes = 0;
            this.score = 0;
            this.position = new Position(pointX, pointY);
        }

        public int Shoes
        {
            get { return this.shoes; }
            set { this.shoes = value; }
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        public int PositionX
        {
            get { return this.position.x; }
            set { this.position.x = value; }
        }

        public int PositionY
        {
            get { return this.position.y; }
            set { this.position.y = value; }
        }
    }
}
