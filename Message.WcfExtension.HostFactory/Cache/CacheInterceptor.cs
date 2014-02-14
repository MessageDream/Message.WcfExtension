using System;
using System.Web;
using System.Web.Caching;
using Ninject.Extensions.Interception;
using YesWay.WcfExtension.Cache;

namespace Message.WcfExtension.HostFactory.Cache
{
    public class CacheInterceptor : SimpleInterceptor
    {
        private static readonly TimeSpan DefaultExpirationTime = new TimeSpan(0, 5, 0);

        private readonly ICacheKeyGenerator _cacheKeyGenerator;

        private readonly TimeSpan _expirationTime;

        private string _cacheKey;
        private bool _haveCache;

        public CacheInterceptor(ICacheKeyGenerator cacheKeyGenerator, TimeSpan expirationTime)
        {
            this._cacheKeyGenerator = cacheKeyGenerator;
            this._expirationTime = expirationTime;
        }

        public CacheInterceptor(ICacheKeyGenerator cacheKeyGenerator)
        {
            this._cacheKeyGenerator = cacheKeyGenerator;
            this._expirationTime = DefaultExpirationTime;
        }


        protected override void BeforeInvoke(IInvocation invocation)
        {
            if (TargetMethodReturnsVoid(invocation)) return;

            _cacheKey = this._cacheKeyGenerator.CreateCacheKey(invocation.Request.Method, invocation.Request.Arguments);
            var cachedResult = (object[])HttpRuntime.Cache.Get(_cacheKey);

            if (cachedResult == null) return;
            invocation.ReturnValue = cachedResult[0];
            _haveCache = true;
        }

        protected override void AfterInvoke(IInvocation invocation)
        {
            if (!_haveCache)
            {
                AddToCache(_cacheKey, invocation.ReturnValue);
            }
            _haveCache = false;
        }

        private static bool TargetMethodReturnsVoid(IInvocation invocation)
        {
            var targetMethod = invocation.Request.Method;
            return targetMethod != null && targetMethod.ReturnType == typeof(void);
        }

        private void AddToCache(string key, object value)
        {
            var cacheValue = new object[] { value };
            HttpRuntime.Cache.Insert(
                key,
                cacheValue,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                this._expirationTime,
                CacheItemPriority.Normal, null);
        }
    }
}
