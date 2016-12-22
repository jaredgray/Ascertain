using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ascertain.Galexy
{
    public class Site 
    {
        public Site() { this.NodeMatches = new List<string>(); }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public List<string> NodeMatches { get; set; }
    }
}
