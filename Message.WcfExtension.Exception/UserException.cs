using System.Runtime.Serialization;

namespace Message.WcfExtension.Exception
{
	/// <summary>
	/// 用户自定义错误
	/// </summary>
	[DataContract]
	public class UserException : System.Exception
	{
		private string _messageValue = "请检查后再试！";
		[DataMember]
		public string Message
		{
			get
			{
				return _messageValue;
			}
			set
			{
				_messageValue = value;
			}
		}
	}
}
