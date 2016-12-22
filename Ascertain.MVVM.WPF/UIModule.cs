using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.MVVM.WPF
{
    public abstract class UIModule : AbstractViewModel, IUIModule
    {
        public string DisplayText
        {
            get
            {
                return _displaytext;
            }
            set
            {
                _displaytext = value;
                base.RaisePropertyChanged("DisplayText");
            }
        }
        private string _displaytext;

        public virtual bool LoadUIOnStartup { get { return false; } }
    }
}
