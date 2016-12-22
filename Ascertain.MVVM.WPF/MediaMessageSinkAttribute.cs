using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascertain.MVVM.WPF
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MediatorMessageSinkAttribute : Attribute
    {
        public MediatorMessageSinkAttribute(string message)
        {
            this.MessageKey = message;
        }

        public object MessageKey { get; private set; }
        public Type ParameterType { get; set; }
    }
}
