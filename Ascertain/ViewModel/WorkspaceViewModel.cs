using Ascertain.Commands;
using Ascertain.Common;
using Ascertain.MVVM;
using Ascertain.MVVM.ViewModel;
using Ascertain.MVVM.WPF;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Ascertain.ViewModel
{
    public class WorkspaceViewModel : BaseViewModel
    {
        public delegate void MenuItemSelectedHandler();

        public WorkspaceViewModel()
        {
            this.ItemSelectedCommand = new RelayCommand(param => this.OnItemSelected());
            this.CurrentTypes = new List<string>();
        }

        //public bool CanAddType(Type space)
        //{

        //}

        public ICommand ItemSelectedCommand { get; protected set; }
        private void OnItemSelected()
        {

        }

        public ObservableCollection<NotificationObject> Spaces
        {
            get
            {
                if (null == this._spaces)
                    this.LoadSpaces();
                return this._spaces;
            }
        }
        private ObservableCollection<NotificationObject> _spaces;

        #region Mediator Commands 

        
        [MediatorMessageSink(MediatorMessages.MenuItemCommand, ParameterType = typeof(ActionItemViewModel))]
        private void ShowViewModel(ActionItemViewModel viewModel)
        {
            ICollectionView vs = CollectionViewSource.GetDefaultView(this.Spaces);
            NotificationObject model = this.Spaces.Where(x => x.GetType() == viewModel.RequestType).FirstOrDefault();
            if (null == model)
            {
                model = (NotificationObject)viewModel.RequestType.CreateModel();
                if (null != model)
                    this.Spaces.Add(model);
            }
            if (null != model)
            {
                vs.MoveCurrentTo(model);
                viewModel.AppliedAction(model);
            }
        }

        #endregion

        private void LoadSpaces()
        {
            if (null == this._spaces)
            {
                this._spaces = new ObservableCollection<NotificationObject>();
                this._spaces.CollectionChanged += _spaces_CollectionChanged;
            }


        }

        private List<string> CurrentTypes { get; set; }

        void _spaces_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (object o in e.NewItems)
                    this.CurrentTypes.Add(o.GetType().ToString());
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (object o in e.OldItems)
                    this.CurrentTypes.Remove(o.GetType().ToString());
            }
        }


    }
}
