using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace YesWay.WcfExtension.Cache
{
    public class DefaultCacheKeyGenerator : ICacheKeyGenerator
    {
        private static readonly Guid _keyGuid = new Guid("ECFD1B0F-0CBA-4AA1-89A0-179B636381CA");
        public string CreateCacheKey(MethodBase method, IEnumerable<object> inputs)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}:", _keyGuid);
            if (method.DeclaringType != null)
            {
                sb.Append(method.DeclaringType.FullName);
            }
            sb.Append(':');
            sb.Append(method.Name);

            if (inputs == null) return sb.ToString();
            //foreach (var input in inputs)
            //{
            //    sb.Append(':');
            //    if (input != null)
            //    {
            //        sb.Append(input.GetHashCode().ToString());
            //    }
            //}
            sb.Append(':');
            var res = BinaryHelper.GetBinaryString(inputs);
            return sb.Append(res.GetHashCode()).ToString();
        }
    }
}
