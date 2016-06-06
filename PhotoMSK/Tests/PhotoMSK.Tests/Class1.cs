using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using PhotoMSK.App_Start;

namespace PhotoMSK.Tests
{
    public class IoCSupportedTest
    {
        private readonly Lazy<IContainer> container = new Lazy<IContainer>(() =>
        {
            var container = AutofacConfig.Register();
            AutomapperConfiguration.Configure(container);
            return container;
        });

        protected TEntity Resolve<TEntity>()
        {
            return container.Value.Resolve<TEntity>();
        }

        protected void ShutdownIoC()
        {
            container.Value.Dispose();
        }
    }
}
