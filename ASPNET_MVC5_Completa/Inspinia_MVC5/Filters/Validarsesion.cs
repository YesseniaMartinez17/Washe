using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Controllers;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Filters
{
    public class Validarsesion : ActionFilterAttribute
    {
        private tbUsuarios vtbUsuarios;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                vtbUsuarios = (tbUsuarios)HttpContext.Current.Session["tbUsuarios"];
                if (vtbUsuarios == null)
                {

                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }



                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }
    }
}