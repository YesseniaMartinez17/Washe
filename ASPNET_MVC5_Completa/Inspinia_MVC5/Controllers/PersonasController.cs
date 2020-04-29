﻿using System;
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
    public class PersonasController : Controller
    {
        private WashEEntities db = new WashEEntities();

        // GET: /Personas/
        public ActionResult Index()
        {
            var tbpersonas = db.tbPersonas.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            return View(tbpersonas.ToList());
        }

        // GET: /Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonas tbPersonas = db.tbPersonas.Find(id);
            if (tbPersonas == null)
            {
                return HttpNotFound();
            }
            return View(tbPersonas);
        }

        // GET: /Personas/Create
        public ActionResult Create()
        {
            ViewBag.per_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            ViewBag.per_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            return View();
        }

        // POST: /Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="per_Id,per_Identidad,per_Nombres,per_Apellidos,per_FechaNacimiento,per_Sexo,per_Telefono,per_CorreoElectronico,per_EstadoCivil,per_Estado,per_RazonInactivo,per_UsuarioCrea,per_FechaCrea,per_UsuarioModifica,per_FechaModifica")] tbPersonas tbPersonas)
        {
            if (ModelState.IsValid)
            {
                db.tbPersonas.Add(tbPersonas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.per_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbPersonas.per_UsuarioCrea);
            ViewBag.per_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbPersonas.per_UsuarioModifica);
            return View(tbPersonas);
        }

        // GET: /Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonas tbPersonas = db.tbPersonas.Find(id);
            if (tbPersonas == null)
            {
                return HttpNotFound();
            }
            ViewBag.per_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbPersonas.per_UsuarioCrea);
            ViewBag.per_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbPersonas.per_UsuarioModifica);
            return View(tbPersonas);
        }

        // POST: /Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="per_Id,per_Identidad,per_Nombres,per_Apellidos,per_FechaNacimiento,per_Sexo,per_Telefono,per_CorreoElectronico,per_EstadoCivil,per_Estado,per_RazonInactivo,per_UsuarioCrea,per_FechaCrea,per_UsuarioModifica,per_FechaModifica")] tbPersonas tbPersonas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbPersonas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.per_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbPersonas.per_UsuarioCrea);
            ViewBag.per_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario", tbPersonas.per_UsuarioModifica);
            return View(tbPersonas);
        }

        // GET: /Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPersonas tbPersonas = db.tbPersonas.Find(id);
            if (tbPersonas == null)
            {
                return HttpNotFound();
            }
            return View(tbPersonas);
        }

        // POST: /Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPersonas tbPersonas = db.tbPersonas.Find(id);
            db.tbPersonas.Remove(tbPersonas);
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