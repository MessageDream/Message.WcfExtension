using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Components;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Injection;
using Ninject.Extensions.Interception.Registry;
using Ninject.Extensions.Interception.Request;
using Ninject.Extensions.Interception.Wrapper;

namespace Message.WcfExtension.HostFactory.Interception.DynamicProxy
{
    public class ExtStandardWrapper : StandardWrapper
    {
        protected ExtStandardWrapper(IKernel kernel, IContext context, object instance)
            : base(kernel, context, instance)
        {
        }

        /// <summary>
        /// 创建一个可执行的调用指定的代理请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>一个可执行的调用指定的代理请求</returns>
        public override IInvocation CreateInvocation(IProxyRequest request)
        {
            IComponentContainer components = request.Context.Kernel.Components;

            IEnumerable<IInterceptor> interceptors = components.Get<IAdviceRegistry>().GetInterceptors(request);
            IMethodInjector injector = components.Get<IInjectorFactory>().GetInjector(request.Method);

            return new ExtInvocation(request, injector, interceptors);
        }
    }
}
