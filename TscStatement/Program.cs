using System;
using  Microsoft.Extensions.DependencyInjection;
using TscStatement.ServiceRealize;

namespace TscStatement
{
    class Program
    {
        private static IServiceProvider _provider;
        static void Main(string[] args)
        {
            IServiceCollection service = new ServiceCollection();
            service.AddServiceCollection(@"C:\Users\Administrator\Desktop\唐三彩下沙保利湾店.xls");
            _provider = service.BuildServiceProvider();
            var context = _provider.GetService<InjectionPoint>();
            context.T1();
        }
    }
}
