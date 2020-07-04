using Inspinia_MVC5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5.Controllers
{
    public class AdministracionController : Controller
    {
        private Helpers function = new Helpers();
        private WashEEntities db = new WashEEntities();

        public ActionResult Suscripciones()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DetallesSuscripcion(int id)
        {
            var detallesS = (
                from x in db.tbSuscripciones
                where x.sus_Id == id
                select x).ToList();

            tbSuscripciones tbSuscripciones = db.tbSuscripciones.Find(id);
            return PartialView(tbSuscripciones);

            //var detallesServicio = (
            //        from x in db.tbServicios
            //        where x.serv_Id == id
            //        select x
            //        );
            //    tbServicios tbServicios = db.tbServicios.Find(id);
            //    return PartialView(tbServicios);

        }

        public JsonResult listarServicios()
        {
            var data = db.tbServicios.Select(
                    serv => new
                    {
                        serv_Id = serv.serv_Id,
                        serv_Descripcion = serv.serv_Descripcion,
                        serv_Estado = serv.serv_Estado

                    }
                ).Where(x => x.serv_Estado == true).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSuscripciones()
        {
            var data = db.tbSuscripciones.Select(
                    suscripciones => new
                    {
                        sus_Id = suscripciones.sus_Id,
                        serv_Idc = suscripciones.serv_Id,
                        sus_CantidadMaximaSolicitudes = suscripciones.sus_CantidadMaximaSolicitudes,
                        sus_Descripcion = suscripciones.sus_Descripcion,
                        sus_Estado = suscripciones.sus_Estado,
                        sus_MesesVigencia = suscripciones.sus_MesesVigencia,
                        sus_Precio = suscripciones.sus_Precio,
                        serv_Descripcion = suscripciones.tbServicios.serv_Descripcion
                    }
                ).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSuscripcion(int sus_Id, string precio, string descripcion, int cantidadM, int vigenciaM)
        {
            var codigo = 500;
            string msj = "";
            try
            {
                int user = (int)Session["Id"];

                var list = db.UDP_prod_tbSuscripciones_Update(sus_Id, Convert.ToDecimal(precio), descripcion, vigenciaM, cantidadM, user, function.DatetimeNow());
                foreach (UDP_prod_tbSuscripciones_Update_Result item in list)
                {
                    msj = " " + item.MensajeError;
                }
                if (Convert.ToInt32(msj) > 1)
                {
                    codigo = 200;
                }
            }
            catch(Exception ex)
            {
                msj = ex.Message;
            }
            return new HttpStatusCodeResult(codigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InhabilitarSuscripcion(int id, string RazonInactivo)
        {
            int codigo = 500;
            string msj = "";
            try
            {
                int idClte = (int)Session["Id"];
                var list = db.UDP_prod_tbSuscripciones_Inhabilitar(id, idClte, function.DatetimeNow(), RazonInactivo);
                foreach (UDP_prod_tbSuscripciones_Inhabilitar_Result item in list)
                {
                    msj = item.MensajeError + " ";
                }
                if (Convert.ToInt32(msj) >= 1)
                {
                    codigo = 200;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return new HttpStatusCodeResult(codigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult habilitarSuscripcion(int id)
        {
            int codigo = 500;
            string msj = "";
            try
            {
                int idClte = (int)Session["Id"];
                var list = db.UDP_prod_tbSuscripciones_Habilitar(id, idClte, function.DatetimeNow());
                foreach (UDP_prod_tbSuscripciones_Habilitar_Result item in list)
                {
                    msj = item.MensajeError + " ";
                }
                if (Convert.ToInt32(msj) >= 1)
                {
                    codigo = 200;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return new HttpStatusCodeResult(codigo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarSuscripcion(int serv_Id, int meses, int cantidad, string serv_Precio, string serv_Descripcion)
        {
            string msj = "";
            int id = (int)Session["Id"];
            try
            {

                string idErrorDup = "";
                bool ins = true;

                string desccc = serv_Descripcion;
                var val = (from v in db.tbSuscripciones where v.sus_Descripcion == desccc select v).ToList();

                if (val.Count > 0)
                {
                    ins = false;
                    idErrorDup += desccc;
                }
                if (ins)
                {
                    var list = db.UDP_prod_tbSuscripciones_Insert(serv_Id, Convert.ToDecimal(serv_Precio), serv_Descripcion, meses, id, function.DatetimeNow(), cantidad);
                    foreach (UDP_prod_tbSuscripciones_Insert_Result item in list)
                    {
                        msj = item.MensajeError + "";
                    }
                }
                else
                {
                    //aux = aux.Remove(aux.Length - 1);
                    idErrorDup = idErrorDup.Remove(idErrorDup.Length - 1);
                    return Json(new { value = true, Message = "Duplicado", Comentario = idErrorDup }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new { value = true, Message = "Error" + ex.ToString() }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
            }

            if (Convert.ToInt32(msj) > 1)
            {
                return Json(new { value = true, Message = "Exito" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { value = true, Message = "Error" + msj.ToString() }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
            }
        }

        public JsonResult ListarSubcategorias(int? id)
        {
            var datos = db.tbServicios.Select(
                                                sub => new
                                                {
                                                    cserv_Id = sub.cserv_Id,
                                                    serv_Id = sub.serv_Id,
                                                    serv_Titulo = sub.serv_Titulo,
                                                    serv_Descripcion = sub.serv_Descripcion,
                                                    serv_Estado = sub.serv_Estado,
                                                    serv_Precio = sub.serv_Precio,
                                                    cserv_Descripcion = sub.tbCategoriaServicios.cserv_Descripcion
                                                }
                                            ).ToList();
            var listado = datos;

            if (id != null)
            {
                listado = (from d in datos where d.cserv_Id == id select d).ToList();
            }
            return Json(listado, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdministrarSubcategorias()
        {
            return View();
        }


        public ActionResult AgregarServicio()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult InsertarServicio(int[] cserv_Id, string[] serv, string[] serv_desc, string[] precios, HttpPostedFileBase[] serv_Image)
        {
            int usuario = (int)Session["Id"];
            string msj = "";
            int l = cserv_Id.Length;
            try
            {
                string idErrorDup = "";
                bool ins = true;

                for (int x = 0; x < l; x++)
                {
                    //Message = "The LINQ expression node type 'ArrayIndex' is not supported in LINQ to Entities."
                    string desccc = serv[x];
                    var val = (from v in db.tbServicios where v.serv_Titulo == desccc select v).ToList();

                    if (val.Count > 0)
                    {
                        ins = false;
                        idErrorDup += serv[x] + ',';
                    }
                }


                if (ins){
                    string ruta = "~/Content/Images/Services/";
                    string path = Server.MapPath(ruta);
                    string extn = "";
                    string filename = "";
                    string final_Name = "";
                    string directorio = "";
                    var date = function.DatetimeNow();
                    for (int x = 0; x < l; x++){

                        filename = Path.GetFileName(serv_Image[x].FileName);
                        extn = Path.GetExtension(filename);

                        final_Name = cserv_Id[x] + "_" + serv[x] + extn;
                        directorio = function.publicarArchivo(serv_Image[x], path, final_Name);

                        if (!directorio.Contains("Content")) throw new Exception();

                        var list = db.UDP_prod_tbServicios_Insert(cserv_Id[x], serv[x], serv_desc[x], Convert.ToDecimal(precios[x]), directorio, function.DatetimeNow(), usuario);
                        foreach (UDP_prod_tbServicios_Insert_Result item in list)
                        {
                            msj = item.MensajeError + " ";
                        }
                        int n = Convert.ToInt32(msj);
                        if (!(n > 1))
                        {
                            throw new Exception();
                        }
                    }
                } else
                {
                    //aux = aux.Remove(aux.Length - 1);
                    idErrorDup = idErrorDup.Remove(idErrorDup.Length - 1);
                    return Json(new { value = true, Message = "Duplicado", Comentario = idErrorDup }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
                }
            }
            catch (Exception ex)
            {
                msj = ex.Message.ToString();
                return Json(new { value = false, Message = "Error" + ex.ToString() }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
            }
            return Json(new { value = true, Message = "Exito" }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
        }
        public ActionResult _DetalleServicios(int id)
        {
            var detallesServicio = (
                from x in db.tbServicios
                where x.serv_Id == id
                select x
                );
            tbServicios tbServicios = db.tbServicios.Find(id);
            return PartialView(tbServicios);
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult obtenerdata()
        {
            List<tbCategoriaServicios> lst;
            lst = (from d in db.tbCategoriaServicios
                   select d).ToList();

            //var categoriasa = db.TipoProductos.OrderBy(a => a.Descripcion)
            //           .Select(c => new { Descripcion = c.Descripcion, Familia = c.Familia, TipoProducto = c.tipoProducto, Activo = c.activo })
            //           .ToList();
            ////            return new JsonResult { Data = categorias, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var categorias = db.tbCategoriaServicios
                                .OrderBy(x => x.cserv_Estado)
                                .Select(x => new
                                {
                                    cserv_Id = x.cserv_Id
                                    ,cserv_Descripcion = x.cserv_Descripcion
                                    ,cserv_Estado = x.cserv_Estado
                                }).ToList();

            return Json(categorias, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CatalogosServicios()
        {
            var tbCat = db.tbCategoriaServicios.OrderBy(x => x.cserv_Estado);
            return View(tbCat.ToList());
        }
        public ActionResult _Detalles(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCategoriaServicios tbCategoriaServicios = db.tbCategoriaServicios.Find(id);
            
            var servicios = (
                                from serv in db.tbServicios
                                where serv.cserv_Id == id
                                select serv
                            ).FirstOrDefault();
            
            if(servicios != null)
            {
                tbCategoriaServicios.tbServicios.Add(servicios);
            }

            if (tbCategoriaServicios == null)
            {
                return HttpNotFound();
            }
            return PartialView(tbCategoriaServicios);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult inactivar(int id, string razon)
        {
            string msj = "";
            var idUsuario = (int)Session["Id"];
            int codigo = 200;// 500;
            var list = db.UDP_Prod_tbCategoriaServicios_Inactivar(
                                                                    id
                                                                    , idUsuario
                                                                    , function.DatetimeNow()
                                                                    , razon
                                                                );
            foreach (UDP_Prod_tbCategoriaServicios_Inactivar_Result item in list)
            {
                msj = item.MensajeError + " ";
                if (Convert.ToInt32(item.MensajeError) > 0)
                {
                    codigo = 200;
                }
            }

            return new HttpStatusCodeResult(codigo);
        }

        [HttpPost]
        public ActionResult Modificar(int id,HttpPostedFileBase fotografia, string titulo, string descripcion, string precio)
        {
            int codigo = 500;
            string msj = "";
            string ruta = "";
            string path = "";
            string extn = "";

            var direcciondb = (from x in db.tbServicios where x.serv_Id == id select x).FirstOrDefault();
            var Pathdb = "~" + direcciondb.serv_Directorio.ToString();
            var rutadb = Server.MapPath(Pathdb);
            decimal Precio = Convert.ToDecimal(precio);

            if (fotografia != null)
            {
                string rutaGrabar = "~/Content/Images/Services/";
                string Opath = Path.GetFileName(fotografia.FileName);
                extn = Path.GetExtension(Opath);

                path = Server.MapPath(rutaGrabar);
                //extn = Path.GetExtension(fotografia);
                //extn = Path.GetExtension(fotografia);
                var cat = (
                                from x in db.tbServicios
                                where x.serv_Id == id
                                select x
                            ).FirstOrDefault();
                bool anterior = function.removerArchivo(rutadb);
                if (anterior)
                {
                    string name = cat.cserv_Id.ToString() + "_" + titulo + extn;
                    ruta = function.publicarArchivo(fotografia, path, name);
                }
            }
            else
            {
                ruta = direcciondb.serv_Directorio.ToString();
            }

            if (ruta.Contains("Content"))
            {
                path = ruta;
                var fecha = function.DatetimeNow();
                var list = db.UDP_prod_tbServicios_Update(
                    id
                    , titulo
                    , descripcion
                    , Precio
                    , path
                    , fecha
                    , Convert.ToInt32(Session["Id"])
                );
                foreach (UDP_prod_tbServicios_Update_Result item in list)
                {
                    msj = item.MensajeError + "";
                }
                if (Convert.ToInt32(msj) > 0)
                {
                    codigo = 200;
                }
            }
            //var list = db.UDP_prod_tbServicios_Insert(
            //                                            serv_Descripcion
            //                                            a
            //     
            return new HttpStatusCodeResult(codigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult inhabilitarServicio(int Id, string RazonInactivo)
        {
            var msj = "";
            try
            {
                var list = db.UDP_prod_tbServicios_Inhabilitar(Id, RazonInactivo, Convert.ToInt32(Session["Id"]), function.DatetimeNow());
                foreach (UDP_prod_tbServicios_Inhabilitar_Result item in list)
                {
                    msj = " " + item.MensajeError;
                }
            }
            catch (Exception ex)
            {
                return Json(new { value = false, Message = "Ha ocurrido un error: " + ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { value = true, Message = "Exito" }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult habilitarServicio(int Id)
        {
            var msj = "";
            try
            {
                var list = db.UDP_prod_tbServicios_Habilitar(Id,(int)Session["Id"],function.DatetimeNow());
                foreach(UDP_prod_tbServicios_Habilitar_Result item in list)
                {
                    msj = item.MensajeError + " ";
                }
            }
            catch (Exception ex)
            {
                return Json(new { value = false, Message = "Ha ocurrido un error: " + ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { value = true, Message = "Exito" }, JsonRequestBehavior.AllowGet);// JsonRequestBehavior(Json)
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult REactivar(int id)
        {
            int codigo = 500;
            string msj = "";
            int idusuario = (int)Session["Id"];

            var list = db.UDP_Prod_tbCategoriaServicios_REactivar(id, idusuario, function.DatetimeNow());

            foreach (UDP_Prod_tbCategoriaServicios_REactivar_Result item in list)
            {
                msj = item.MensajeError + " ";
                if (Convert.ToInt32(item.MensajeError) > 0)
                {
                    codigo = 200;
                }
            }

            return new HttpStatusCodeResult(codigo);
        }
    }
}