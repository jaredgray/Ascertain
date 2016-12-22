using Ascertain.Common;
using Ascertain.MVVM.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Ascertain
{
    public class Bootstrapper
    {
        public Bootstrapper() { }

        public void Load()
        {
            ModuleInfo.ModuleTypes = new List<Type>();
            List<string> modulesdefinitions = System.Configuration.ConfigurationManager.AppSettings["Modules"].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            modulesdefinitions.ForEach(m =>
            {
                Assembly assm = Assembly.Load(m);
                List<TypeInfo> modules = assm.DefinedTypes.Where(x => x.ImplementedInterfaces.Contains(typeof(IUIModule))).ToList();
                modules.ForEach(mod =>
                {
                    ModuleInfo.ModuleTypes.Add(mod);
                });
                string[] resourcenames = assm.GetManifestResourceNames();
                string datatemplate = resourcenames.Where(x => x.Contains("DataTemplates")).FirstOrDefault();
                if (null != datatemplate)
                {
                    Stream resourcestream = assm.GetManifestResourceStream(datatemplate);
                    ResourceDictionary dict = (ResourceDictionary)XamlReader.Load(resourcestream);
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
              
            });
        }

        private void LoadResource(Type t)
        {
           
        }
    }
}
