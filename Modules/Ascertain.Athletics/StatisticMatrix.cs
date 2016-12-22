using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Athletics
{
    public class StatisticMatrix
    {
        public StatisticMatrix()
        {
            this.Statistics = new List<Statistic>();
        }

        public List<Statistic> Statistics
        {
            get;
            set;
        }

        public List<string> ToCsv(bool produceheader)
        {
            List<string> csv = new List<string>();
            if (null == this.Statistics || this.Statistics.Count <= 0)
                return csv;
            if (produceheader)
            {
                Statistic top = this.Statistics[0];
                csv.Add(this.CreateHeading(top));
            }
            this.Statistics.ForEach(x =>
            {
                csv.Add(this.CreateLine(x));
            });
            return csv;
        }

        private string CreateHeading(Statistic statistic)
        {
            StringBuilder rowbuilder = new StringBuilder();
            statistic.fields.ForEach(x =>
            {
                rowbuilder.Append("\"");
                rowbuilder.Append(x.Name);
                rowbuilder.Append("\"");
                rowbuilder.Append(",");
            });
            return rowbuilder.ToString().TrimEnd(",".ToCharArray());
        }

        private string CreateLine(Statistic statistic)
        {
            StringBuilder rowbuilder = new StringBuilder();
            statistic.fields.ForEach(x =>
            {
                rowbuilder.Append("\"");
                rowbuilder.Append(x.Value);
                rowbuilder.Append("\"");
                rowbuilder.Append(",");
            });
            return rowbuilder.ToString().TrimEnd(",".ToCharArray());
        }

    }
}
