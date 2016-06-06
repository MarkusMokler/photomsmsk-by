using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public abstract class AbstractService
    {
        readonly Lazy<AppContext> _lazy = new Lazy<AppContext>(() => new AppContext());

        protected readonly MiniProfiler MiniProfiler = MiniProfiler.Current;
        protected AppContext Context => _lazy.Value;


        public void Dispose()
        {
            if (_lazy.IsValueCreated)
            {
                Context.Dispose();
            }
        }
   
    }
}
