using Ascertain.Mine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Galexy
{
    public class FetchResponse
    {
        public FetchResponse() { }

        public HtmlNodeCollection SelectedResult
        {
            get;
            set;
        }
    }
}
