using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.MVVM.WPF
{
    public abstract class AbstractViewModel : NotificationObject
    {
        public AbstractViewModel()
        {
            this.Mediator.Register(this);
        }

        #region Mediator

        public Mediator Mediator
        {
            get
            {
                return Mediator.Instance;
            }
        }

        #endregion
    }
}
