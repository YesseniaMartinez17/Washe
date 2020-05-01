using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class Helpers
    {
        WashEEntities db = new WashEEntities();


        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }

    }
}