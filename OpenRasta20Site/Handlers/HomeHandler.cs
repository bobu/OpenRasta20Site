using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;
using OpenRasta20Site.Resources;

namespace OpenRasta20Site.Handlers
{
    public class HomeHandler
    {
        public object Get()
        {
            Thread.Sleep(6000);
            return new Home { Title = "GET." };
        }

        public object Post()
        {
            return new Home { Title = "POST." };
        }

        public object Put()
        {
            return new Home { Title = "PUT." };
        }

        public object Delete()
        {
            return new Home { Title = "DELETE." };
        }
    }
}