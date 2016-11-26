using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaGuffin
{
    class Interaction
    {
        private int numTimes;
        private String text;

        public Interaction(int n, String t)
        {
            numTimes = n;
            text = t;
        }

        public int getNumTimes() { return numTimes; }
        public void decNumTimes() { if(numTimes > 0) numTimes -= 1; }
        public Boolean validInteraction() { return numTimes != 0; }
        public String getText() { return text; }
    }
}
