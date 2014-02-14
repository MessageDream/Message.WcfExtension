using System.Collections.Generic;
using System.Reflection;

namespace YesWay.WcfExtension.Cache
{   
   public  interface ICacheKeyGenerator
    {
        string CreateCacheKey(MethodBase method, IEnumerable<object> inputs);
    }
}
