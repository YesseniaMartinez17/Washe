using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Filters;
namespace Inspinia_MVC5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Validarsesion());
        }
    }
}
