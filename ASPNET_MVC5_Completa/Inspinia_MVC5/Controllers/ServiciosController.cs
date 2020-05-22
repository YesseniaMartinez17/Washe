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
        private WashEEntities db = new WashEEntities();

        // GET: /Servicios/
        public ActionResult externo()
        {
            var tbservicios = db.ServicioExterno;/*.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1)*/
            return View(tbservicios.ToList());
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
        public ActionResult solicitar(string id, string desc, string precio)
        {
            //var idr = id.Split();
            var arrDesc = desc.Split(',');
            var arrId = id.Split(',');
            var arrprecios = precio.Split(',');

            int l = arrDesc.Length;
            int l1 = arrId.Length;

            ViewBag.CantS = l;
            ViewBag.idS = arrId;
            ViewBag.descripcionesS = arrDesc;
            ViewBag.Precios = arrprecios;

            return View();
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

        // POST: /Servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
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
