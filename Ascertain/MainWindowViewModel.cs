using Ascertain.Common;
using Ascertain.MVVM;
using Ascertain.MVVM.ViewModel;
using Ascertain.MVVM.WPF;
using Ascertain.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ascertain
{
    /*
     http://sports.yahoo.com/golf/pga/leaderboard/2013/1
     * 1455.83
     */
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            _progressindicatorvisibility = Visibility.Collapsed;
        }

        #region Binding interface

        public Visibility ProgressIndicatorVisibility
        {
            get
            {
                return _progressindicatorvisibility;
            }
            set
            {
                _progressindicatorvisibility = value;
                base.RaisePropertyChanged("ProgressIndicatorVisibility");
            }
        }
        private Visibility _progressindicatorvisibility;

        #endregion

        public ObservableCollection<MenuItemViewModel> Menu
        {
            get
            {
                if (null == this._menu)
                    this.LoadMenu();
                return this._menu;
            }
        }
        private ObservableCollection<MenuItemViewModel> _menu;

        private void LoadMenu()
        {
            this._menu = new ObservableCollection<MenuItemViewModel>();
            MenuItemViewModel item = new MenuItemViewModel() { Text = "FILE" };
            //item.Children.Add(new MenuItemViewModel() { Text = "New Location", RequestType = typeof(LocationViewModel), AppliedAction = (x) => { ((LocationViewModel)x).SelectedItemIndex = 1; } });

            ModuleInfo.ModuleTypes.ForEach(t =>
            {
                IUIModule mod = (IUIModule)Activator.CreateInstance(t);
                item.Children.Add(new MenuItemViewModel()
                {
                    RequestType = mod.GetType(),
                    Text = mod.DisplayText,
                    AppliedAction = (x) => { }
                });

            });
            this._menu.Add(item);
        }

        public Microsoft.Practices.Prism.ViewModel.NotificationObject WorkSpace
        {
            get
            {
                if (null == _workspace)
                    _workspace = new WorkspaceViewModel();
                return _workspace;
            }
            set
            {
                this._workspace = value;
            }
        }
        private Microsoft.Practices.Prism.ViewModel.NotificationObject _workspace;


        public ObservableCollection<Microsoft.Practices.Prism.ViewModel.NotificationObject> ActionItems
        {
            get
            {
                if (null == this._actionitems)
                    this.LoadActionItems();
                return this._actionitems;
            }
        }
        private ObservableCollection<Microsoft.Practices.Prism.ViewModel.NotificationObject> _actionitems;

        private void LoadActionItems()
        {
            this._actionitems = new ObservableCollection<Microsoft.Practices.Prism.ViewModel.NotificationObject>();
            ModuleInfo.ModuleTypes.ForEach(t =>
            {
                IUIModule mod = (IUIModule)Activator.CreateInstance(t);
                this._actionitems.Add(new ActionItemViewModel()
                {
                    RequestType = mod.GetType(),
                    Text = mod.DisplayText,
                    AppliedAction = (x) => { }
                });

            });
        }


        [MediatorMessageSink(MediatorMessages.ShowView, ParameterType = typeof(Microsoft.Practices.Prism.ViewModel.NotificationObject))]
        private void ShowViewModel(Microsoft.Practices.Prism.ViewModel.NotificationObject viewModel)
        {
            this.WorkSpace = viewModel;
            base.RaisePropertyChanged("WorkSpace");
        }


        [MediatorMessageSink(CommonMediatorMessages.OnBeginProgress, ParameterType = typeof(Microsoft.Practices.Prism.ViewModel.NotificationObject))]
        private void BeginProgress(Microsoft.Practices.Prism.ViewModel.NotificationObject viewModel)
        {
            this.ProgressIndicatorVisibility = Visibility.Visible;
        }

        [MediatorMessageSink(CommonMediatorMessages.OnEndProgress, ParameterType = typeof(Microsoft.Practices.Prism.ViewModel.NotificationObject))]
        private void EndProgress(Microsoft.Practices.Prism.ViewModel.NotificationObject viewModel)
        {
            this.ProgressIndicatorVisibility = Visibility.Collapsed;
        }
    }
}
