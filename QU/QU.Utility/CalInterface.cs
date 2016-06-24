using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility.CalInterface
{
    public class AlterationQuery
    {
        public string RawQuery;
        public string CustomAugmentation;
    }

    public class AlterationResponse
    {
        public List<AlterationQuery> AlterationQueryList;
        public List<AlterationQuery> RawQueryVariantList;
    }
}
