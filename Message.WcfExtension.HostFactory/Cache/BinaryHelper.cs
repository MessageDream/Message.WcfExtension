using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace YesWay.WcfExtension.Cache
{
    public static class BinaryHelper
    {
        public static string GetBinaryString(IEnumerable<object> obj)
        {
            string res;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                res = Convert.ToBase64String(buffer);
            }
            return res;
        }
    }
}
