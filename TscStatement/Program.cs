using System;
using System.IO;
using System.Security.Policy;
using  Microsoft.Extensions.DependencyInjection;
using TscStatement.ServiceRealize;

namespace TscStatement
{
    class Program
    {
        private static IServiceProvider _provider;
        static void Main(string[] args)
        {
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (!Directory.Exists(deskPath + @"\唐三彩对账单"))
            {
                Console.WriteLine("桌面不存在唐三彩对账单文件夹。");
                Console.WriteLine("请按任意键退出。");
                Console.ReadKey(true);
                return;
            }


            string[] files = Directory.GetFiles(deskPath + @"\唐三彩对账单");

            IServiceCollection service = new ServiceCollection();
            service.AddServiceCollection(files[0]);
            _provider = service.BuildServiceProvider();
            var context = _provider.GetService<InjectionPoint>();
            context.T1(files);
            Console.WriteLine("对账单汇总表完毕。");
            Console.WriteLine("按任意键退出。");
        }
    }
}
