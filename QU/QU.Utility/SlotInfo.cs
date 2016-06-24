using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    public class SlotInfo
    {
        public int NBest;
        public string Tag;
        public string Text;
        public double Score;
    }

    public class ChunkInfo
    {
        private string feature;

        public string Feature
        {
            get { return feature; }
            set { feature = value; }
        }
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
