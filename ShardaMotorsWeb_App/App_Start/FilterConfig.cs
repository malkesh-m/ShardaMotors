using System.Web;
using System.Web.Mvc;

namespace ShardaMotorsWeb_App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
