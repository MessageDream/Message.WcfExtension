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
        /// ��ȡ����Χ��
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object GetRequestScope(IContext context)
        {
            return OperationContext.Current;
        }

        /// <summary>
        /// �������ʵ��
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
        /// ֹͣ���ʵ��
        /// </summary>
        public void Stop()
        {
        }

        public object RequestScope { get; private set; }


        /// <summary>
        /// ����һ���ں˰�һ��serviceHost�������
        /// �ṩ�Լ���ServiceHostʵ��,������������Ͱ�����ʵ�ֵ�ServiceHost�ࡣ
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