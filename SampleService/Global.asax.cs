using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Message.WcfExtension.HostFactory.Interception.DynamicProxy;
using Ninject;
using Ninject.Web.Common;

namespace SampleService
{
    public class Global : NinjectHttpApplication
    {
        /// <summary>
        /// 创建一个注入模块管理应用程序
        /// </summary>
        /// <returns></returns>
        protected override IKernel CreateKernel()
        {
            //return 
            var kernel = new StandardKernel(new NinjectSettings { LoadExtensions = false }, new IOCModule(), new DynamicProxyModule());
            return kernel;
        }
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}