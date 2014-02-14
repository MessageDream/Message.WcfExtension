using System.Collections.Generic;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Injection;
using Ninject.Extensions.Interception.Invocation;
using Ninject.Extensions.Interception.Request;

namespace Message.WcfExtension.HostFactory.Interception.DynamicProxy
{
    public class ExtInvocation : InvocationBase
    {
        public ExtInvocation(IProxyRequest request, IMethodInjector injector, IEnumerable<IInterceptor> interceptors)
            : base(request, interceptors)
        {
            Injector = injector;
        }

        public IMethodInjector Injector { get; protected set; }


        protected override object CallTargetMethod()
        {
            return this.ReturnValue ?? Injector.Invoke(Request.Target, Request.Arguments);
        }
    }
}
