using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.Commands
{
    public static class Extensions
    {
        public static Microsoft.Practices.Prism.ViewModel.NotificationObject CreateModel(this Type t)
        {
            return (Microsoft.Practices.Prism.ViewModel.NotificationObject)Activator.CreateInstance(t);
        }
    }
}