using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Mine
{
    public class Request
    {
        public static HtmlNode GetDocumentNode(string url)
        {
            HtmlWeb wb = new HtmlWeb();
            HtmlDocument doc = wb.Load(url);          
            return doc.DocumentNode;
        }

    }
}
