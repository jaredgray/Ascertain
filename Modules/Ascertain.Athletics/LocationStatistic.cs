using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Athletics
{
    public abstract class LocationStatistic : Statistic
    {
        public LocationStatistic(Player player)
            : base(player)
        {

        }

        #region Public interface 

        public string Location
        {
            get
            {
                return base.GetValue("Location").Value;
            }
            set
            {
                base.SetValue("Location", value);
            }
        }

        #endregion
    }
}
