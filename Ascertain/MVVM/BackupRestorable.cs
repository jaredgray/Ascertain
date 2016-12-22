using Ascertain.MVVM.ViewModel;
using Ascertain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.MVVM
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BackupRestoreableAttribute : Attribute
    {
        public void Backup(object target)
        {
            if (!(target is BaseViewModel))
                throw new Exception("Backup and Restore is only available on type BaseViewModel. You must inherit from this class");
            target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => 0 < x.GetCustomAttributes(typeof(RestorablePropertyAttribute), true).Count()).ToList().ForEach(pi =>
            {
                dynamic item = target;
                object value = pi.GetValue(target, null);
                item.Properties[string.Format("{0}Saved", pi.Name)] = value;
            });
        }

        public void Restore(object target)
        {
            if (!(target is BaseViewModel))
                throw new Exception("Backup and Restore is only available on type BaseViewModel. You must inherit from this class");
            target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => 0 < x.GetCustomAttributes(typeof(RestorablePropertyAttribute), true).Count()).ToList().ForEach(pi =>
            {
                dynamic item = target;
                object value = item.Properties[string.Format("{0}Saved", pi.Name)];
                pi.SetValue(target, value, null);

            });
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class RestorablePropertyAttribute : Attribute
    {
        public RestorablePropertyAttribute()
        {

        }
    }
}
