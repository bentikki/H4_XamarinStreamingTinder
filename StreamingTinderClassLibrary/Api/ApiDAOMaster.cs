using StreaminTinderClassLibrary.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingTinderClassLibrary.Api
{
    internal abstract class ApiDAOMaster
    {
        protected ApiRequester apiRequester;
        protected string apiString;

        public ApiDAOMaster(string apidestination)
        {
            this.apiString = apidestination;
            this.apiRequester = new ApiRequester(apidestination);
        }
    }
}
