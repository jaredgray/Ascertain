using Ascertain.Commands;
using Ascertain.Common;
using Ascertain.MVVM.ViewModel;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ascertain.ViewModel
{
    public class ActionItemViewModel : BaseViewModel
    {
        public ActionItemViewModel() 
        {
            this.ActionCommand = new RelayCommand(x => this.OnActionCommand());
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                base.RaisePropertyChanged("Text");
            }
        }
        private string _text;

        public Thickness Margin
        {
            get
            {
                return _margin;
            }
            set
            {
                _margin = value;
                base.RaisePropertyChanged("Margin");
            }
        }
        private Thickness _margin;

        public Type RequestType
        {
            get;
            set;
        }

        public Action<NotificationObject> AppliedAction { get; set; }

        public ICommand ActionCommand
        {
            get;
            set;
        }
        protected void OnActionCommand()
        {
            if (null == this.RequestType)
                return;
            base.Mediator.NotifyColleagues(MediatorMessages.MenuItemCommand, this);
        }
    }
}