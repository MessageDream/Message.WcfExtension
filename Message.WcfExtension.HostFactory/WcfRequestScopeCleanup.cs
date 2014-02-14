using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Ninject;
using Ninject.Activation.Caching;


namespace Message.WcfExtension.HostFactory
{
    using Message = System.ServiceModel.Channels.Message;
    /// <summary>
    /// 每次请求后在当前执行上下文中清除Ninject缓存
    /// </summary>
    public class WcfRequestScopeCleanup : GlobalKernelRegistration, IDispatchMessageInspector
    {
        /// <summary>
        ///标识是否在每次请求最后释放
        /// </summary>
        private readonly bool _releaseScopeAtRequestEnd;


        public WcfRequestScopeCleanup(bool releaseScopeAtRequestEnd)
        {
            this._releaseScopeAtRequestEnd = releaseScopeAtRequestEnd;
        }

        /// <summary>
        /// 一个入站消息已收到但是在消息被派到预定的操作之前触发
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext">当前服务实例</param>
        /// <returns></returns>
        public virtual object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        /// <summary>
        /// 发送回复消息之前触发
        /// </summary>
        /// <param name="reply">如果是单向的这个值为null</param>
        /// <param name="correlationState">AfterReceiveRequest方法中的返回值</param>
        public virtual void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (this._releaseScopeAtRequestEnd)
            {
                var context = OperationContext.Current;
                this.MapKernels(kernel => kernel.Components.Get<ICache>().Clear(context));
            }
        }
    }
}