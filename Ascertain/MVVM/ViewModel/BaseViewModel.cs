using Ascertain.MVVM.WPF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.MVVM.ViewModel
{
    public abstract class BaseViewModel : AbstractViewModel
    {
        public BaseViewModel()
        {
        }

        #region Backup/Restore Model

        public void Backup()
        {
            BackupRestoreableAttribute instance = this.GetType().GetCustomAttributes(typeof(BackupRestoreableAttribute), true).FirstOrDefault() as BackupRestoreableAttribute;
            if (null != instance)
                instance.Backup(this);
        }

        public void Restore()
        {
            BackupRestoreableAttribute instance = this.GetType().GetCustomAttributes(typeof(BackupRestoreableAttribute), true).FirstOrDefault() as BackupRestoreableAttribute;
            if (null != instance)
                instance.Restore(this);
        }

        public Hashtable Properties
        {
            get
            {
                if (null == _properties)
                    _properties = new Hashtable();
                return _properties;
            }
        }
        private Hashtable _properties;

        #endregion

        #region State Assignment 

        public bool IsDirty
        {
            get;
            set;
        }

        #endregion

        #region Overrides 

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            this.IsDirty = true;
        }

        #endregion
    }
}
