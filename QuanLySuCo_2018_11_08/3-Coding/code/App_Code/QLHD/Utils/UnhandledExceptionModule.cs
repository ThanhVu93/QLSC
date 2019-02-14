using DotNetNuke.Instrumentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace QLHD
{
    /// <summary>
    /// The HttpModule catches any unhandled exception by IIS and passes it to Log4NET.
    /// </summary>
    /// <remarks>
    /// Logging can be disabled by setting 'LogUnhandledExceptions' in app.config or web.config to 'false'. Alternatively, the HttpModule
    /// can simply be removed. It is possible to install the module on IIS as a global managed module, so that all unhandled exceptions
    /// for all methods can be logged. Use the files in the \Install folder to see how.
    /// </remarks>
    public class UnhandledExceptionModule : IHttpModule
    {
        private readonly ILog logger = LoggerSource.Instance.GetLogger(typeof(UnhandledExceptionModule).FullName);
        private bool logUnhandeldExceptions;

        public void Init(HttpApplication context)
        {
            bool success = bool.TryParse(ConfigurationManager.AppSettings["LogUnhandledExceptions"], out logUnhandeldExceptions);
            if (!success)
            {
                logUnhandeldExceptions = true;
            }

            context.Error += new EventHandler(OnError);
        }

        public void Dispose()
        {
            
        }


        private void OnError(object sender, EventArgs e)
        {
            try
            {
                if (!logUnhandeldExceptions) { return; }

                string userIp;
                string url;
                string exception;

                HttpContext context = HttpContext.Current;

                if (context != null)
                {
                    userIp = context.Request.UserHostAddress;
                    url = context.Request.Url.ToString();

                    // get last exception, but check if it exists
                    Exception lastException = context.Server.GetLastError();

                    if (lastException != null)
                    {
                        exception = lastException.ToString();
                    }
                    else
                    {
                        exception = "no error";
                    }
                }
                else
                {
                    userIp = "no httpcontext";
                    url = "no httpcontext";
                    exception = "no httpcontext";
                }

                logger.ErrorFormat("Unhandled exception occured. UserIp [{0}]. Url [{1}]. Exception [{2}]", userIp, url, exception);
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured in OnError: [{0}]", ex);
            }
        }

    }
}