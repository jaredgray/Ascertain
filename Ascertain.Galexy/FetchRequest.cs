using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ascertain.Galexy
{
    public class FetchRequest
    {
        public string SiteUrl { get; set; }

        public Regex TargetNodeMatch { get; set; }

        public string XPath { get; set; }

        public Type NodeType { get; set; }
    }
}
