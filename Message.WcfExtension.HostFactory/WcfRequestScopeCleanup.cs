using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Ninject;
using Ninject.Activation.Caching;


namespace Message.WcfExtension.HostFactory
{
    using Message = System.ServiceModel.Channels.Message;
    /// <summary>
    /// ÿ��������ڵ�ǰִ�������������Ninject����
    /// </summary>
    public class WcfRequestScopeCleanup : GlobalKernelRegistration, IDispatchMessageInspector
    {
        /// <summary>
        ///��ʶ�Ƿ���ÿ����������ͷ�
        /// </summary>
        private readonly bool _releaseScopeAtRequestEnd;


        public WcfRequestScopeCleanup(bool releaseScopeAtRequestEnd)
        {
            this._releaseScopeAtRequestEnd = releaseScopeAtRequestEnd;
        }

        /// <summary>
        /// һ����վ��Ϣ���յ���������Ϣ���ɵ�Ԥ���Ĳ���֮ǰ����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext">��ǰ����ʵ��</param>
        /// <returns></returns>
        public virtual object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        /// <summary>
        /// ���ͻظ���Ϣ֮ǰ����
        /// </summary>
        /// <param name="reply">����ǵ�������ֵΪnull</param>
        /// <param name="correlationState">AfterReceiveRequest�����еķ���ֵ</param>
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