using Ascertain.Athletics;
using Ascertain.Galexy;
using Ascertain.Mine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Golf
{
    public class Yahoo : Site
    {
        public Yahoo()
        {
            base.Name = "Yahoo";
            base.NodeMatches.Add(@"leaderboardtable");
            base.NodeMatches.Add(@"playerScorecard");
        }

        public static StatisticMatrix GetScoreboardStatistic(HtmlNode scoreboard, Player player, Fetch fetch)
        {
            StatisticMatrix matrix = new StatisticMatrix();
            if (null == scoreboard || null == scoreboard.ChildNodes || scoreboard.ChildNodes.Count <= 0)
                return matrix;
            RoundStatistic CurrentRound = null;
            int iteration = 0;
            foreach (HtmlNode node in scoreboard.ChildNodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    if (node.Name == "h4")
                    {
                        CurrentRound = new RoundStatistic(player);
                        matrix.Statistics.Add(CurrentRound);
                        CurrentRound.Round = node.InnerText;
                    }
                    else if (node.Name == "table")
                    {
                        ParseRoundData(CurrentRound, node, fetch, player, iteration);
                    }
                }
            }
            return matrix;
        }

        private static void ParseRoundData(RoundStatistic current, HtmlNode table, Fetch fetch, Player player, int iteration)
        {
            List<HtmlNode> rows = fetch.SelectNodes(table.ChildNodes, "tr");

            int rowindex = 2;
            HtmlNode statrow = rows[rowindex];

            // if the iteration is 0 then we want to 

            List<HtmlNode> cells = fetch.SelectNodes(statrow.ChildNodes, "td");
            int cnt = 0, offset = 0;
            cells.ForEach(x =>
            {
                if (cnt == 0)
                {
                    current.SetValue("LastName - FirstName", string.Format("{0} {1}", player.LastName, player.FirstName));
                    current.SetValue("FirstName", player.FirstName);
                    current.SetValue("LastName", player.LastName);                    
                    current.SetValue("Round Shorthand", x.InnerText);
                }
                else
                {
                    if (cnt != 10 && cnt != 20)
                    {
                        current.SetValue(string.Format("Round {0}", cnt-offset), x.InnerText);
                    }
                    else
                        offset++;
                }
                cnt++;
            });
        }
    }
}
