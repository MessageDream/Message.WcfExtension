namespace Message.WcfExtension.HostFactory.Log
{
    public interface ILog
    {
        #region 记录Log

        /// <summary>
        /// Error级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        void ToError(object msg);

        /// <summary>
        /// Error级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常对象</param>
        void ToError(object msg, System.Exception ex);

        /// <summary>
        /// Debug级别的日志
        /// </summary>
        /// <param name="msg">日志信息.</param>
        void ToDebug(object msg);

        /// <summary>
        /// Debug级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常对象</param>
        void ToDebug(object msg, System.Exception ex);

        /// <summary>
        /// Info级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        void ToInfo(object msg);

        /// <summary>
        /// Info级别的日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常对象</param>
        void ToInfo(object msg, System.Exception ex);

        void ToFatal(object msg, System.Exception ex);

        #endregion
    }
}
