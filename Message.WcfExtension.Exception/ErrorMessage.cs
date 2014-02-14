using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Message.WcfExtension.Exception
{
    /// <summary>
    /// 错误信息
    /// </summary>
    [DataContract(Name = "ErrorMessage")]
    public class ErrorMessage
    {
        private string _textValue;
        private string _suggestionValue;
        /// <summary>
        /// 错误编码
        /// </summary>
        [DataMember]
        public int ErrorCode
        {
            get;
            set;
        }
        /// <summary>
        /// 错误文本
        /// </summary>
        [DataMember]
        public string Text
        {
            get
            {
                if (string.IsNullOrEmpty(_textValue))
                {
                    ErrorMessage error = StoredErrorMessage.FirstOrDefault(ele => ele.ErrorCode == ErrorCode);
                    if (error != null)
                    {
                        return error.Text;
                    }
                }

                return _textValue;
            }
            set
            {
                _textValue = value;
            }
        }
        /// <summary>
        /// 更正错误建议
        /// </summary>
        [DataMember]
        public string Suggestion
        {
            get
            {
                if (string.IsNullOrEmpty(_suggestionValue))
                {
                    ErrorMessage error = StoredErrorMessage.FirstOrDefault(ele => ele.ErrorCode == ErrorCode);
                    if (error != null)
                    {
                        return error.Suggestion;
                    }
                }

                return _suggestionValue;
            }
            set
            {
                _suggestionValue = value;
            }
        }

        public ErrorMessage()
        {
        }

        public ErrorMessage(int errorCode, string text, string suggestion)
        {
            ErrorCode = errorCode;
            Text = text;
            Suggestion = suggestion;
        }

        public ErrorMessage(ErrorCode code, string text, string suggestion)
        {
            ErrorCode = (int)code;
            Text = text;
            Suggestion = suggestion;
        }

        public static IList<ErrorMessage> StoredErrorMessage = new List<ErrorMessage>
		{
			new ErrorMessage(Exception.ErrorCode.Success,string.Empty, string.Empty),
			new ErrorMessage(Exception.ErrorCode.RequestIdentityRefused,"请求身份被拒绝", "请检查身份信息是否正确！"),
			new ErrorMessage(Exception.ErrorCode.NoPermissions,"请求身份无权限", "您没有适当的权限，建议您先开通后使用！"),
			new ErrorMessage(Exception.ErrorCode.ServiceNotExist,"服务不存在, 或待开发", "^-^！"),
			new ErrorMessage(Exception.ErrorCode.ServiceError,"请求的服务类型错误", "请查看您的服务类型是否正确?"),
			new ErrorMessage(Exception.ErrorCode.ExcuteTimeOut,"服务执行超时", "网络状况可能不太好， 您稍后再试一试！"),
			new ErrorMessage(Exception.ErrorCode.SystemError,"系统错误", "系统故障，系统可能处于检修或其他状态，建议您咨询一下！"),
			new ErrorMessage(Exception.ErrorCode.ArgumentError,"参数错误", "你请求的参数可能不对，请检查后再试！"),
            new ErrorMessage(Exception.ErrorCode.ArgumentNotNull,"必要参数不能为空","请查看您请求的必要参数是不是为空值"),
			new ErrorMessage(Exception.ErrorCode.XMLError,"XML文件格式错误，无法解析正确的消息", "你请求的XML格式可能不对，请检查后再试！")
		};

        public static ErrorMessage GetStoredErrorMessage(ErrorCode errorCode)
        {
            return StoredErrorMessage.FirstOrDefault(ele => ele.ErrorCode == (int)errorCode);
        }
    }
    [DataContract]
    public enum ErrorCode
    {
        /// <summary>
        /// 执行成功, 默认值
        /// </summary>
        [EnumMember]
        Success = 0,
        /// <summary>
        /// 请求身份被拒绝
        /// </summary>
        [EnumMember]
        RequestIdentityRefused = 101,
        /// <summary>
        /// 请求身份无权限
        /// </summary>
        [EnumMember]
        NoPermissions = 102,
        /// <summary>
        /// 服务不存在, 或待开发
        /// </summary>
        [EnumMember]
        ServiceNotExist = 103,
        /// <summary>
        /// 请求的服务类型错误
        /// </summary>
        [EnumMember]
        ServiceError = 104,
        /// <summary>
        /// 服务执行超时
        /// </summary>
        [EnumMember]
        ExcuteTimeOut = 105,
        /// <summary>
        /// 系统错误
        /// </summary>
        [EnumMember]
        SystemError = 106,
        /// <summary>
        /// 参数错误
        /// </summary>
        [EnumMember]
        ArgumentError = 107,
        /// <summary>
        /// XML文件格式错误，无法解析正确的消息
        /// </summary>
        [EnumMember]
        XMLError = 108,
        /// <summary>
        /// 参数不能为空
        /// </summary>
        [EnumMember]
        ArgumentNotNull = 109
    }
}
