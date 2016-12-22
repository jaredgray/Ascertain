using Ascertain.Mine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Athletics
{
    public class Player
    {
        public Player() { this.Name = string.Empty; }
        public Player(bool lastnamefirst) : this() { this.LastNameFirst = lastnamefirst; }

        #region Public interface 

        public string Name { get { return string.IsNullOrEmpty(_name) ? string.Empty : _name.Trim(); } set { _name = value; } }
        private string _name;

        public string FirstName { get { return this.GetFirstName(); } }
        private string GetFirstName()
        {
            string name = this.Name;
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            if (!this.LastNameFirst)
            {
                return name.Split(' ')[0];
            }
            Suffixes.Split('|').ToList().ForEach(x =>
            {
                name = name.Replace(x, "");
            });
            string[] names = name.Split(' ');
            if (names.Length > 1)
                return names[1];
            return string.Empty;
        }

        public string LastName { get { return this.GetLastName(); } }
        private string GetLastName()
        {
            string name = this.Name;
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            if (this.LastNameFirst)
            {
                return name.Split(' ')[0];
            }
            Suffixes.Split('|').ToList().ForEach(x =>
            {
                name = name.Replace(x, "");
            });
            string[] names = name.Split(' ');
            if (names.Length > 1)
                return names[1];
            return string.Empty;
        }

        public StatisticMatrix ScoreCard
        {
            get;
            set;
        }

        #endregion

        #region private interface 

        private bool LastNameFirst { get; set; }


        const string Suffixes = @"Sr|Sr.|Jr|Jr.";
        #endregion
    }
}
