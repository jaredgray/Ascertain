using Ascertain.Mine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ascertain.Galexy
{
    public class Fetch
    {
        public Fetch(FetchRequest rqst)
        {
            this.Request = rqst;
        }

        #region public interface 

        public HtmlNode DocumentNode
        {
            get
            {
                if (null == _documentnode)
                {
                    if (!string.IsNullOrEmpty(this.Request.SiteUrl))
                    {
                        _documentnode = Mine.Request.GetDocumentNode(Request.SiteUrl);
                    }
                }
                return _documentnode;
            }
            set
            {
                _documentnode = value;
            }
        }
        private HtmlNode _documentnode;

        public FetchResponse FetchResults()
        {            
            return new FetchResponse()
            {
                SelectedResult = this.DocumentNode.SelectNodes(this.Request.XPath)
            };           
        }

        public HtmlNode SelectNode(string idmatch)
        {
            return Mine.Miner.GetNodeByMatch(this.DocumentNode, idmatch);
        }

        public List<HtmlLink> SelectLinks(string baseurl, HtmlNodeCollection collection)
        {
            List<HtmlNode> nodes = this.SelectNodes(collection, "a");
            List<HtmlLink> result = new List<HtmlLink>();
            nodes.ForEach(node =>
            {
                result.Add(new HtmlLink(baseurl, node.OuterHtml));
            });
            return result;
        }
        
        public List<HtmlNode> SelectNodes(HtmlNodeCollection collection, string nodetype)
        {
            List<HtmlNode> result = new List<HtmlNode>();
            foreach (HtmlNode node in collection)
            {
                if (node.NodeType == HtmlNodeType.Element && node.Name == nodetype)
                    result.Add(node);
                result.AddRange(this.FindNodes(node, nodetype));
            }
            return result;
        }

        public List<HtmlNode> FindNodes(HtmlNode parent, string nodetype)
        {
            List<HtmlNode> result = new List<HtmlNode>();
            if (null == parent || null == parent.ChildNodes || parent.ChildNodes.Count <= 0)
                return new List<HtmlNode>();
            
            foreach (HtmlNode node in parent.ChildNodes)
            {
                if (node.NodeType == HtmlNodeType.Element && node.Name == nodetype)
                {
                    result.Add(node);
                }
                if (null != node.ChildNodes && node.ChildNodes.Count > 0)
                {
                    result.AddRange(this.SelectNodes(node.ChildNodes, nodetype));
                }
            }
            return result;
        }

        #endregion

        #region properties

        public FetchRequest Request
        {
            get;
            set;
        }

        #endregion
    }
}
