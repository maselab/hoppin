using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameSystem;

namespace hoppin
{
    abstract class AbstractPlayer
    {
        private String name;
        abstract public PlayerMove move(); 

        public String Name
        {
            get { return this.name; }
        }
    }
}
