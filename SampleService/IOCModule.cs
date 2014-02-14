using Message.WcfExtension.HostFactory;
using Ninject.Web.Common;

namespace SampleService
{
    public class IOCModule : WcfExtensionModule
    {
        /// <summary>
        ///加载模块到注入内核容器
        /// </summary>
        public override void Load()
        {
            base.Load();
            this.Bind<IService1>().To<Service1>().InRequestScope();

        }
    }
}