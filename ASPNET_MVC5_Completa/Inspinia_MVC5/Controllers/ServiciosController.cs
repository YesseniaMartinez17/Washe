using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5.Models;

namespace Inspinia_MVC5.Controllers
{
    public class ServiciosController : Controller
    {
        Models.Helpers Function = new Models.Helpers();
        private WashEEntities db = new WashEEntities();

        // GET: /Servicios/
        public ActionResult externo()
        {

            var tbservicios = db.ServicioExterno;/*.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1)*/
            return View(tbservicios.ToList());
        }

        ////http://localhost:58893/Servicios/adquirirSuscripcion/1

        public ActionResult adquirirSuscripcion(int id)
        {
            var tbsus = db.tbSuscripciones.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1).ToList();
            //var tbpersonas = db.tbPersonas.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            List<tbSuscripciones> lst;
            lst = (
                    from p in db.tbSuscripciones
                    select p).ToList();

            string idsServ = (from l in lst select l.serv_Id).ToString();
            int idServInt = Convert.ToInt32(idsServ);
            ViewBag.descServ = (from x in db.tbServicios where x.serv_Id == idServInt select x.serv_Descripcion);

            return View(tbsus);
        }

        // GET: /Servicios/
        public ActionResult externodetalles(int id)
        {
            var tbservicios = (from x in db.ServicioExterno where x.Id == id select x).ToList();

            var suscripciones = (from y in db.tbSuscripciones where y.serv_Id == id select y);
            if(suscripciones.Count() > 1)
            {
                ViewBag.suscripcionValida = true;
            }
            else
            {
                ViewBag.suscripcionValida = false;
            }
                return PartialView(tbservicios);
        }


        // GET: /Servicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbServicios tbServicios = db.tbServicios.Find(id);
            if (tbServicios == null)
            {   
                return HttpNotFound();
            }
            return View(tbServicios);
        }
        public ActionResult solicitar(string id/*, string desc, string precio*/)
        {

            var ListaIDs = id.Split(',');
            var l = ListaIDs.Length;
            int IDProd;
            //CREAR UNA LISTA TIPADA
            List<VM_ServicioSolicitud> ListaCarrito = new List<VM_ServicioSolicitud>();
            //INSTANCIAR EL VIEWMODEL VM_CarritoDeCompra PARA AGREGAR LOS REGISTROS A LA LISTA "ListaCarrito" DENTRO DEL BUCLE 
            //ITERAR LA LISTA DE IDS RECIBIDAS EN LA ACCIÓN 
            for (int x = 0;x<l;x++)
            {
                VM_ServicioSolicitud Carrito = new VM_ServicioSolicitud();

                //CONVERSIÓN EXPLICITA DE LOS ID DE STRING A TIPO INT
                IDProd = Convert.ToInt32(ListaIDs[x]);

                //CREAR UNA LISTA GENERICA CON LOS CAMPOS (ID, Descripcion, Precio)
                var GenericList = (from dbSerExt in db.ServicioExterno
                                   where dbSerExt.Id == IDProd
                                   select new
                                   {
                                       Id = IDProd,
                                       Descripcion = dbSerExt.Descripcion,
                                       Precio = dbSerExt.Precio
                                   }).First();
                //SETAR LA INSTANCIA Carrito
                Carrito.id = GenericList.Id;
                Carrito.descripcion = GenericList.Descripcion;
                Carrito.precio = GenericList.Precio;
                //AGREGAR EL REGISTRO DE LA INSTANCIA Carrito A LA LISTA ListaCarrito TIPO List<VM_CarritoDeCompra>
                ListaCarrito.Add(Carrito);
            }
            //foreach (string ID in ListaIDs)
            //{
            //    VM_ServicioSolicitud Carrito = new VM_ServicioSolicitud();

            //    //CONVERSIÓN EXPLICITA DE LOS ID DE STRING A TIPO INT
            //    IDProd = Convert.ToInt32(ID);

            //    //CREAR UNA LISTA GENERICA CON LOS CAMPOS (ID, Descripcion, Precio)
            //    var GenericList = (from dbSerExt in db.ServicioExterno
            //                       where dbSerExt.Id == IDProd
            //                       select new
            //                       {
            //                           Id = IDProd,
            //                           Descripcion = dbSerExt.Descripcion,
            //                           Precio = dbSerExt.Precio
            //                       }).First();
            //    //SETAR LA INSTANCIA Carrito
            //    Carrito.id = GenericList.Id;
            //    Carrito.descripcion = GenericList.Descripcion;
            //    Carrito.precio = GenericList.Precio;
            //    //AGREGAR EL REGISTRO DE LA INSTANCIA Carrito A LA LISTA ListaCarrito TIPO List<VM_CarritoDeCompra>
            //    ListaCarrito.Add(Carrito);
            //}
            ViewBag.CantS = l;
            ViewBag.Lids = id;
            //RETORNAR VIEW
            return View(ListaCarrito);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SolicitarPedido(string[] IdPedidos,int[] Cantidades, string Lat, string Lng, string ubicacion )
        {
            string msj = "";

            var codigo = 500;

            decimal total = 0;
            try
            {

                int Client_Id = Convert.ToInt16(Session["Id"]);
                int l = IdPedidos.Length;
                decimal[] precios = new decimal[l];

                for (int c = 0; c < l; c++)
                {
                    int idServ = Convert.ToInt32(IdPedidos[c]);
                    int CantServ = Cantidades[c];



                    var precioservicio = (
                                        from datos in db.ServicioExterno
                                        where datos.Id == idServ
                                        select datos.Precio
                                    ).First();

                    total += (Convert.ToDecimal(precioservicio) * CantServ);
                    precios[c] = Convert.ToDecimal(precioservicio);
                }
                var cliente = (
                                    from datosCliente in db.tbUsuarios
                                    where datosCliente.usu_Id == Client_Id
                                    select datosCliente.per_Id
                                ).First();
                //insertamos
                var list = db.UDP_Fact_tbPedidos_Insert(
                                                    Convert.ToInt32(cliente)
                                                    , DateTime.Now
                                                    , ubicacion
                                                    , Lat
                                                    , Lng
                                                    , total);
                //validamos y reetornamso id
                foreach (UDP_Fact_tbPedidos_Insert_Result item in list)
                {
                    msj = item.MensajeError + " ";
                }

                //obtenemos el id
                int id = Convert.ToInt32(msj);


                //cpomienza la ejecucion
                for (int c2 = 0; c2 < l; c2++)
                {
                    var list2 = db.UDP_Fact_tbPedidosDetalle_Insert(
                                                                    id
                                                                    , Convert.ToInt32(IdPedidos[c2])
                                                                    , precios[c2]
                                                                    );
                    foreach (UDP_Fact_tbPedidosDetalle_Insert_Result item2 in list2)
                    {
                        msj = item2.MensajeError + " ";
                    }
                }
                codigo = 200;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return new HttpStatusCodeResult(codigo);
        }

        public ActionResult Mapa()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.serv_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            ViewBag.serv_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            return View();
        }
        public ActionResult Pendientes()
        {
            var tbPedidos = db.tbPedidos.Where(s => s.pdidos_estado == "1"); ;/*.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1)*/
            return View(tbPedidos.ToList());
            //ViewBag.serv_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            //ViewBag.serv_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
        }
        
        public ActionResult _PedidosDetalle(int? id)
        {
            //var cliente = (
            //                    from datosCliente in db.tbUsuarios
            //                    where datosCliente.usu_Id == Client_Id
            //                    select datosCliente.per_Id
            //                ).First();

            var detalles = (
                                from resultados in db.tbPedidosDetalle
                                where resultados.pdidos_Id == id
                                select resultados
                            );
            tbPedidosDetalle pedidoDetalle = db.tbPedidosDetalle.Find(id);
            return PartialView(detalles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="serv_Id,serv_Precio,serv_Descripcion,serv_Estado,serv_RazonInactivo,serv_UsuarioCrea,serv_FechaCrea,serv_UsuarioModifica,serv_FechaModifica")] tbServicios tbServicios)
        {
            if (ModelState.IsValid)
            {
                db.tbServicios.Add(tbServicios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.serv_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbServicios.serv_UsuarioCrea);
            ViewBag.serv_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbServicios.serv_UsuarioModifica);
            return View(tbServicios);
        }

        // GET: /Servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbServicios tbServicios = db.tbServicios.Find(id);
            if (tbServicios == null)
            {
                return HttpNotFound();
            }
            ViewBag.serv_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbServicios.serv_UsuarioCrea);
            ViewBag.serv_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbServicios.serv_UsuarioModifica);
            return View(tbServicios);
        }

        // POST: /Servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="serv_Id,serv_Precio,serv_Descripcion,serv_Estado,serv_RazonInactivo,serv_UsuarioCrea,serv_FechaCrea,serv_UsuarioModifica,serv_FechaModifica")] tbServicios tbServicios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbServicios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.serv_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbServicios.serv_UsuarioCrea);
            ViewBag.serv_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbServicios.serv_UsuarioModifica);
            return View(tbServicios);
        }

        // GET: /Servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbServicios tbServicios = db.tbServicios.Find(id);
            if (tbServicios == null)
            {
                return HttpNotFound();
            }
            return View(tbServicios);
        }

        // POST: /Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbServicios tbServicios = db.tbServicios.Find(id);
            db.tbServicios.Remove(tbServicios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
