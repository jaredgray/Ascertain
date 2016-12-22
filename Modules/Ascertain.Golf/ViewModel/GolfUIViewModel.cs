using Ascertain.Athletics;
using Ascertain.Galexy;
using Ascertain.Mine;
using Ascertain.MVVM.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ascertain.Golf.ViewModel
{
    public class GolfUIViewModel : UIModule
    {
        public GolfUIViewModel()
        {
            base.DisplayText = "Golf Stats";
            this.GetReportsCommand = new Microsoft.Practices.Prism.Commands.DelegateCommand(() => this.OnGetReportsCommand());
        }

        #region Binding UI Display Interface

        public string SelectedUrl
        {
            get
            {
                return _selectedurl;
            }
            set
            {
                _selectedurl = value;
                base.RaisePropertyChanged("SelectedUrl");
            }
        }
        private string _selectedurl;

        public Site SelectedItem
        {
            get
            {
                return _selecteditem;
            }
            set
            {
                _selecteditem = value;
                //base.RaisePropertyChanged("SelectedItem");
            }
        }
        private Site _selecteditem;

        public ObservableCollection<Site> AvailableItems
        {
            get
            {
                if (null == _availableitems)
                    this.LoadAvailableItems();
                return _availableitems;
            }
        }
        private ObservableCollection<Site> _availableitems;

        public string StatusText
        {
            get
            {
                return _statustext;
            }
            set
            {
                _statustext = value;
                base.RaisePropertyChanged("StatusText");
            }
        }
        private string _statustext;

        #endregion

        #region Command Interface

        public ICommand GetReportsCommand { get; set; }


        string ATag = @"<a id="""" href=""/golf/pga/players/Dustin+Johnson/9267/scorecard/2013/1"">Dustin Johnson</a>";

        Regex atagparser = new Regex("href=\"(?<Link>(.*))\">(?<Text>(.*))</a>");

        private void OnGetReportsCommand()
        {
            base.Mediator.NotifyColleagues(CommonMediatorMessages.OnBeginProgress, this);
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                this.StatusText = "Retrieving results...";
                try
                {
                    //System.Threading.Thread.Sleep(5000);
                    this.OnGetReportsCommandInternal();
                    this.StatusText = "Complete..";
                }
                catch 
                {
                    this.StatusText = "Oops.. an error occurred";
                }

            }).ContinueWith(x =>
            {
                base.Mediator.NotifyColleagues(CommonMediatorMessages.OnEndProgress, this);
            });
        }

        private void OnGetReportsCommandInternal()
        {
            string loc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<string> files = Directory.GetFiles(loc, "*.csv").ToList();
            files.ForEach(x =>
            {
                try { File.Delete(x); }
                catch { }
            });

            FetchRequest rqst = new FetchRequest()
            {
                SiteUrl = this.SelectedUrl,
                XPath = "/html[1]/body[1]/div[1]/div[2]/div[1]/div[3]/table[1]/tbody[1]"
            };
            Fetch fetch = new Fetch(rqst);
            FetchResponse response = fetch.FetchResults();
            List<HtmlLink> links = fetch.SelectLinks(this.SelectedUrl, response.SelectedResult);

            List<Player> players = new List<Player>();
            links.ForEach(x =>
            {
                Player player = this.PlayerFromLink(x);
                //TODO set sort field name for statistic sorting (Sorting on the round short name)
                player.ScoreCard.Statistics.Sort(Statistic.SortByField("Round Shorthand"));
                players.Add(player);
                
            });

            players.OrderBy(x => x.ScoreCard).ThenBy(n => n.LastName);


            using (StreamWriter sw = new StreamWriter("YahooPlayerStats.csv"))
            {
                bool applyheader = true;
                players.ForEach(x =>
                {
                    List<string> csv = x.ScoreCard.ToCsv(applyheader);
                    applyheader = false;
                    csv.ForEach(line =>
                    {
                        sw.WriteLine(line);
                    });
                });
            }
            string name = string.Format("{0}.csv", Guid.NewGuid().ToString());
            File.Copy("YahooPlayerStats.csv", name, true);
            Process.Start(name);

        }


        #endregion

        #region private interface

        private Player PlayerFromLink(HtmlLink link)
        {
            Player p = new Player() { Name = link.Text };

            FetchRequest rqst = new FetchRequest()
            {
                SiteUrl = link.Url,
                XPath = "/html[1]/body[1]/div[1]/div[2]/div[1]/div[3]/table[1]/tbody[1]"
            };
            Fetch fetch = new Fetch(rqst);
            HtmlNode node = fetch.SelectNode(this.SelectedItem.NodeMatches[1]);
            // /html[1]/body[1]/div[1]/div[2]/div[2]
            // /html[1]/body[1]/div[1]/div[2]/div[2]/h4[1]

            //http://sports.yahoo.com/golf/pga/leaderboard/2013/1

            // HtmlNode h4 = node.SelectSingleNode("/h4[1]");

            p.ScoreCard = Yahoo.GetScoreboardStatistic(node, p, fetch);


            return p;
        }

        private void LoadAvailableItems()
        {
            this._availableitems = new ObservableCollection<Site>();
            this._availableitems.Add(new Yahoo());
            //Add other sites here

            this.SelectedItem = this._availableitems.First();
        }

        #endregion
    }
}
