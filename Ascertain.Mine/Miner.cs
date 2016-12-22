using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ascertain.Mine
{
    public class Miner
    {
        public static HtmlNode GetNodeByMatch(HtmlNode parentNode, string match)
        {
            if (null != parentNode.Attributes && null != parentNode.Attributes["id"] && parentNode.Attributes["id"].Value == match)
                return parentNode;
            if (null != parentNode.ChildNodes)
            {
                foreach (HtmlNode node in parentNode.ChildNodes)
                    if (node.NodeType == HtmlNodeType.Element)
                    {
                        HtmlNode candidate = GetNodeByMatch(node, match);
                        if (null != candidate)
                            return candidate;
                    }
            }
            return null;
        }

    }
}
