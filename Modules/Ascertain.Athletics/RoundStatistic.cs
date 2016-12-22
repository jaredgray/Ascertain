using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Athletics
{
    public class RoundStatistic: Statistic
    {
        public RoundStatistic(Player player)
            : base(player)
        {

        }

        #region Public interface 

        public string Round
        {
            get
            {
                return base.GetValue("Round").Value;
            }
            set
            {
                base.SetValue("Round", value);
            }
        }

        #endregion
    }
}
