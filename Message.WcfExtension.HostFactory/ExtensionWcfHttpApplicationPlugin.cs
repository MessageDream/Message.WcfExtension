using System.Linq;
using System.ServiceModel;
using Message.WcfExtension.HostFactory.ServiceHost;
using Ninject;
using Ninject.Activation;
using Ninject.Components;
using Ninject.Web.Common;

namespace Message.WcfExtension.HostFactory
{
    public class ExtensionWcfHttpApplicationPlugin : NinjectComponent, INinjectHttpApplicationPlugin
    {
        private readonly IKernel kernel;

        public ExtensionWcfHttpApplicationPlugin(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// 获取请求范围。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object GetRequestScope(IContext context)
        {
            return OperationContext.Current;
        }

        /// <summary>
        /// 启动这个实例
        /// </summary>
        public void Start()
        {
            BaseExtensionServiceHostFactory.SetKernel(this.kernel);
            BaseExtensionServiceSelfHostFactory.SetKernel(this.kernel);
            //#if !MONO
            //            ExtensionDataServiceHostFactory.SetKernel(this.kernel);
            //#endif
            this.RegisterCustomBehavior();
        }

        /// <summary>
        /// 停止这个实例
        /// </summary>
        public void Stop()
        {
        }

        public object RequestScope { get; private set; }


        /// <summary>
        /// 创建一个内核绑定一个serviceHost如果你想
        /// 提供自己的ServiceHost实现,覆盖这个方法和绑定您的实现的ServiceHost类。
        /// </summary>
        protected virtual void RegisterCustomBehavior()
        {
            if (!this.kernel.GetBindings(typeof(System.ServiceModel.ServiceHost)).Any())
            {
                this.kernel.Bind<System.ServiceModel.ServiceHost>().To<ExtensionServiceHost>();
            }
        }
    }
}