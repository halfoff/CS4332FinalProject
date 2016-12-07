using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaGuffin
{
    class Interaction
    {
        private int numTimes;
        private String[] text;
        private String itemTaken, itemGiven;

        public Interaction(int n, String[] t, String i1, String i2)
        {
            numTimes = n;
            text = t;
            itemTaken = i1;
            itemGiven = i2;
        }

        public int getNumTimes() { return numTimes; }
        public void decNumTimes() { if(numTimes > 0) numTimes -= 1; }
        public Boolean validInteraction() { return numTimes != 0; }
        public String[] getText() { return text; }
        public String getItemDesired() { return itemTaken; }
        public Boolean validItemDesired() { return itemTaken.Length > 0; }
        public String getItemGiven() { return itemGiven; }
        public Boolean validItemGiven() { return itemGiven.Length > 0; }
    }
}
