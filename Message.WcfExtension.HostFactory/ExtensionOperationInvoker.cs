using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using Message.WcfExtension.Exception;
using Message.WcfExtension.HostFactory.Log;

namespace Message.WcfExtension.HostFactory
{
    /// <summary>
    /// 方法拦截器
    /// </summary>
    public class ExtensionOperationInvoker : IOperationInvoker
    {
        private readonly IOperationInvoker _invoker;
        private readonly ILog _log;
        private readonly string _operationName;

        public ExtensionOperationInvoker(IOperationInvoker invoker, ILog log, string operationName)
        {
            _invoker = invoker;
            _log = log;
            _operationName = operationName;
        }

        public object[] AllocateInputs()
        {
            return _invoker.AllocateInputs();
        }

        /// <summary>
        /// 调用拦截
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            var log = new StringBuilder();
            object returnedValue = null;
            var outputParams = new object[] { };

            try
            {
                log.AppendLine("调用方法" + _operationName + "开始。");
                PreInvoke(instance, inputs, ref log);

                returnedValue = _invoker.Invoke(instance, inputs, out outputParams);
                outputs = outputParams;
                return returnedValue;
            }
            catch (BusinessException be)
            {
                _log.ToError("调用方法" + _operationName + "异常。", be);
                throw new FaultException<ErrorMessage>(be.ErrorMessage, new FaultReason(be.ErrorMessage.Text), new FaultCode(be.ErrorMessage.ErrorCode.ToString()));
            }
            catch (System.Exception ex)
            {
                _log.ToFatal("调用方法" + _operationName + "异常。", ex);
                throw new FaultException<ErrorMessage>(ErrorMessage.GetStoredErrorMessage(ErrorCode.SystemError), new FaultReason(ex.Message), new FaultCode(((int)ErrorCode.SystemError).ToString()));
            }
            finally
            {
                PostInvoke(instance, returnedValue, outputParams, ref log);
                log.AppendLine("调用方法" + _operationName + "结束。");
                _log.ToInfo(log.ToString());
            }
        }

        private void PreInvoke(object instance, IEnumerable<object> inputs, ref StringBuilder log)
        {
            log.AppendLine("输入参数:");
            foreach (object argument in inputs)
            {
                if (argument != null)
                {
                    log.AppendLine(argument.ToString());
                }
                else
                {
                    log.AppendLine("null");
                }
            }
        }

        private void PostInvoke(object instance, object returnedValue, object[] outputs, ref StringBuilder log)
        {
            log.AppendLine("返回值：");
            log.AppendLine((returnedValue ?? "null").ToString());
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            var log = new StringBuilder();

            try
            {
                log.AppendLine("异步调用方法" + _operationName + "请求开始。");
                PreInvoke(instance, inputs, ref log);

                return _invoker.InvokeBegin(instance, inputs, callback, state);
            }
            catch (System.Exception ex)
            {
                _log.ToFatal("异步调用方法" + _operationName + "请求异常。", ex);
                throw;
            }
            finally
            {
                log.AppendLine("异步调用方法" + _operationName + "请求完毕。");
                _log.ToInfo(log.ToString());
            }
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            var log = new StringBuilder();
            object returnedValue = null;
            object[] outputParams = { };

            try
            {
                log.AppendLine("异步调用方法" + _operationName + "结束开始。");
                returnedValue = _invoker.InvokeEnd(instance, out outputs, result);
                outputs = outputParams;
                return returnedValue;
            }
            catch (System.Exception ex)
            {
                _log.ToFatal("异步调用方法" + _operationName + "结束异常。", ex);
                throw;
            }
            finally
            {
                log.AppendLine("异步调用方法" + _operationName + "结束完毕。");
                PostInvoke(instance, returnedValue, outputParams, ref log);
                _log.ToInfo(log.ToString());
            }
        }

        public bool IsSynchronous
        {
            get { return _invoker.IsSynchronous; }
        }
    }
}
