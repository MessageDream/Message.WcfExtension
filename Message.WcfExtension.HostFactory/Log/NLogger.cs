using NLog;

namespace Message.WcfExtension.HostFactory.Log
{
    public class NLogger : ILog
    {
        private readonly Logger _logger;

        public NLogger(Logger logger)
        {
            this._logger = logger;
        }

        #region 判断Log等级
        public bool IsDebug
        {
            get
            {
                return _logger.IsDebugEnabled;
            }
        }

        public bool IsInfo
        {
            get
            {
                return _logger.IsInfoEnabled;
            }
        }

        public bool IsError
        {
            get
            {
                return _logger.IsErrorEnabled;
            }
        }
        public bool IsFatal
        {
            get
            {
                return _logger.IsFatalEnabled;
            }
        }
        #endregion

        #region 记录Log
        /// <summary>
        /// Error级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public void ToError(object msg)
        {
            _logger.Error(msg);
        }

        /// <summary>
        /// Error级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常对象</param>
        public void ToError(object msg, System.Exception ex)
        {
            _logger.Error(msg.ToString() + " [Exception]{0}", ex);
        }

        /// <summary>
        /// Debug级别的日志
        /// </summary>
        /// <param name="msg">日志信息.</param>
        public void ToDebug(object msg)
        {
            _logger.Debug(msg);

        }

        /// <summary>
        /// Debug级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常对象</param>
        public void ToDebug(object msg, System.Exception ex)
        {
            _logger.Debug(msg.ToString() + " [Exception]{0}", ex);
        }

        /// <summary>
        /// Info级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public void ToInfo(object msg)
        {
            _logger.Info(msg);
        }

        /// <summary>
        /// Info级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常对象</param>
        public void ToInfo(object msg, System.Exception ex)
        {
            _logger.Info(msg.ToString() + " [Exception]{0}", ex);
        }

        public void ToFatal(object msg, System.Exception ex)
        {
            _logger.Fatal(msg.ToString() + " [Exception]{0}", ex);
        }
        #endregion
    }
}
