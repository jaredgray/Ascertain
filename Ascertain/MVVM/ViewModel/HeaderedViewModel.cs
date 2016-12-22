using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.MVVM.ViewModel
{
    public class HeaderedViewModel : BaseViewModel
    {
        public HeaderedViewModel() { }


        public string HeaderText
        {
            get
            {
                return this._headertext;
            }
            set
            {
                this._headertext = value;
                base.RaisePropertyChanged("HeaderText");
            }
        }
        private string _headertext;
    }
}
