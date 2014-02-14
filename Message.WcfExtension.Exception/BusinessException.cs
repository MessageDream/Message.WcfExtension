using System.Runtime.Serialization;

namespace Message.WcfExtension.Exception
{
    [DataContract]
	public class BusinessException : System.Exception
	{
        [DataMember]
		public ErrorMessage ErrorMessage
		{
			get;
			set;
		}

		public BusinessException()
			: base()
		{
            ErrorMessage = new ErrorMessage((int)ErrorCode.SystemError, string.Empty, string.Empty);
        }

		public BusinessException(string message)
			: base(message)
		{
            ErrorMessage = new ErrorMessage((int)ErrorCode.SystemError, message,string.Empty);
        }

        public BusinessException(int errorCode ,string message)
            : base(message)
        {
            ErrorMessage = new ErrorMessage(errorCode, message, string.Empty);
        }

        public BusinessException(int errorCode, string message, string suggestion)
            : base(message)
        {
            ErrorMessage = new ErrorMessage(errorCode, message, suggestion);
        }

		public BusinessException(ErrorMessage em)
		{
            ErrorMessage = em;          
		}
	}
}