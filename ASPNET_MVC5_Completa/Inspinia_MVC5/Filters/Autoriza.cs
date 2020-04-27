using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Filters
{
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class Autoriza : AuthorizeAttribute
        {
            private tbUsuarios otbUsuarios;
            private WashEEntities db = new WashEEntities();
            private int idOperacion;

            public Autoriza(int idOperacion = 0)
            {
                this.idOperacion = idOperacion;
            }


            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                String nombreOperacion = "";
                String nombreModulo = "";
                try
                {
                    otbUsuarios = (tbUsuarios)HttpContext.Current.Session["tbUsuarios"];
                //Session["tbUsuarios"] = null;salir
                var lstMisOperaciones = from m in db.tbAccionRol
                                            where m.rol_Id == otbUsuarios.rol_id
                                                && m.accion_Id == idOperacion
                                            select m;


                    if (lstMisOperaciones.ToList().Count() == 0)
                    {
                        var oOperacion = db.tbAcciones.Find(idOperacion);
                        //int? idModulo = oOperacion.idModulo;
                        nombreOperacion = getNombreDeOperacion(idOperacion);
                        //nombreModulo = getNombreDelModulo(idModulo);
                        filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + /*nombreModulo*/"" + "&msjeErrorExcepcion=");
                    }
                }
                catch (Exception ex)
                {
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + /*nombreModulo*/"" + "&msjeErrorExcepcion=" + ex.Message);
                }
            }

            public string getNombreDeOperacion(int idOperacion)
            {
                var ope = from op in db.tbAcciones
                          where op.accion_Id == idOperacion
                          select op.accion_descripcion;
                String nombreOperacion;
                try
                {
                    nombreOperacion = ope.First();
                }
                catch (Exception)
                {
                    nombreOperacion = "";
                }
                return nombreOperacion;
            }

            //public string getNombreDelModulo(int? idModulo)
            //{
            //    var modulo = from m in db.modulo
            //                 where m.id == idModulo
            //                 select m.nombre;
            //    String nombreModulo;
            //    try
            //    {
            //        nombreModulo = modulo.First();
            //    }
            //    catch (Exception)
            //    {
            //        nombreModulo = "";
            //    }
            //    return nombreModulo;
            //}

        }
}