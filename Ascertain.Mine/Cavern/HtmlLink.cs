using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ascertain.Mine
{
    public class HtmlLink
    {
        public HtmlLink(string baselink, string linknode)
        {
            this.Url = string.Empty;
            this.Text = string.Empty;
            this.ParseLink(baselink, linknode);
        }

        #region Public interface 

        public string Url { get; set; }

        public string Text { get; set; }


        public string RawLink
        {
            get;
            set;
        }

        #endregion

        #region Parse 

        private void ParseLink(string baselink, string linknode)
        {
            this.RawLink = linknode;
            Uri uri = new Uri(baselink);
            string host = uri.AbsoluteUri.Replace(uri.AbsolutePath, "");
            

            Match m = atagparser.Match(linknode);
            Group linkgroup = m.Groups["Link"];
            Group textgroup = m.Groups["Text"];

            if (linkgroup.Success)
            {
                this.Url = string.Format("{0}/{1}", host, linkgroup.Value.Trim('/'));
            }

            if (textgroup.Success)
            {
                this.Text = textgroup.Value;
            }
        }

        Regex atagparser = new Regex("href=\"(?<Link>(.*))\">(?<Text>(.*))</a>");
        #endregion
    }
}
