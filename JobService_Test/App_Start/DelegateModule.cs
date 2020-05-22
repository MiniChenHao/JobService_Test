using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobService_Test
{
    /// <summary>
    /// 
    /// </summary>
    public class DelegateModule : Autofac.Module
    {
        private Func<IEnumerable<Assembly>> _assemblyProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyProvider"></param>
        public DelegateModule(Func<IEnumerable<Assembly>> assemblyProvider)
        {
            if (assemblyProvider == null) throw new ArgumentNullException(nameof(assemblyProvider));

            _assemblyProvider = assemblyProvider;
        }
    }
}
