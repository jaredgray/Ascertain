using Ascertain.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.ViewModel
{
    public class MenuItemViewModel : ActionItemViewModel
    {
        public MenuItemViewModel() { }
        
        public ObservableCollection<MenuItemViewModel> Children
        {
            get
            {
                if (null == this._children)
                    this._children = new ObservableCollection<MenuItemViewModel>();
                return this._children;
            }
        }
        private ObservableCollection<MenuItemViewModel> _children;
    }
}
