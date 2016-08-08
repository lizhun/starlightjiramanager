using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Main
{
    public class WebClientEx : WebClient
    {

        public WebClientEx()
        : this(new CookieContainer())
        { }
        public WebClientEx(CookieContainer c)
        {
            this.CookieContainer = c;
        }
        public CookieContainer CookieContainer { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = this.CookieContainer;
            }
            return request;
        }
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            string setCookieHeader = response.Headers[HttpResponseHeader.SetCookie];

            if (!string.IsNullOrEmpty(setCookieHeader))
            {
                //do something if needed to parse out the cookie.
                try
                {
                    Cookie cookie = new Cookie();
                    cookie.Domain = request.RequestUri.Host;
                    this.CookieContainer.Add(cookie);

                }
                catch (Exception)
                {

                }
            }
            return response;
        }
    }
}
