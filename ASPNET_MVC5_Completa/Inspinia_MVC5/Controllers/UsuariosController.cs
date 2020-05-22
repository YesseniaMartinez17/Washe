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
    public class UsuariosController : Controller
    {
        private WashEEntities db = new WashEEntities();

        // GET: /Usuarios/
        public ActionResult Index()
        {
            var tbusuarios = db.tbUsuarios.Include(t => t.tbPersonas).Include(t => t.tbRoles);
            return View(tbusuarios.ToList());
        }

        // GET: /Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuarios);
        }

        // GET: /Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad");
            ViewBag.rol_id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion");
            return View();
        }

        // POST: /Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {

            //if (file != null)
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        file.InputStream.CopyTo(ms);
            //        byte[] array = ms.GetBuffer();

            //        var context = new Models.BDEntities();
            //        context.activofijoes.Add(new Models.activofijo()
            //        {
            //            FotoAF = array,
            //        });
            //        context.SaveChanges();
            //    }

            //}
            return View();
        }

        // GET: /Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbUsuarios.per_Id);
            ViewBag.rol_id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion", tbUsuarios.rol_id);
            return View(tbUsuarios);
        }

        // POST: /Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="usu_Id,usu_NombreDeUsuario,usu_Contrasenia,usu_BloqueoActivo,rol_id,per_Id,usu_Fotografia")] tbUsuarios tbUsuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.per_Id = new SelectList(db.tbPersonas, "per_Id", "per_Identidad", tbUsuarios.per_Id);
            ViewBag.rol_id = new SelectList(db.tbRoles, "rol_Id", "rol_Descripcion", tbUsuarios.rol_id);
            return View(tbUsuarios);
        }

        // GET: /Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            if (tbUsuarios == null)
            {
                return HttpNotFound();
            }
            return View(tbUsuarios);
        }

        // POST: /Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsuarios tbUsuarios = db.tbUsuarios.Find(id);
            db.tbUsuarios.Remove(tbUsuarios);
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
