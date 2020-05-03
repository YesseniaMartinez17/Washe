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
        Models.Helpers Function = new Models.Helpers();

        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
        public ActionResult Index()
        {
            var tbpersonas = db.tbPersonas.Include(t => t.tbUsuarios).Include(t => t.tbUsuarios1);
            List<tbPersonas> lst;
            lst = (
                            from p in db.tbPersonas
                            select p
                        ).ToList();
            
            return View(tbpersonas.ToList());
        }

        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
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
            return PartialView(tbPersonas);
        }
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
        public ActionResult Create()
        {
            //Ddl Sexo
            var Sexo = new List<object> { };
            Sexo.Add(new { Id = "", Descripcion = "**Seleccione una opción**" });
            Sexo.Add(new { Id = "F", Descripcion = "Femenino" });
            Sexo.Add(new { Id = "M", Descripcion = "Masculino" });
            Sexo.Add(new { Id = "I", Descripcion = "Indiferente" });
            //Ddl EstadoCivil
            var EstadoCivil = new List<object> { };
            EstadoCivil.Add(new { Id = "", Descripcion = "**Seleccione una opción**" });
            EstadoCivil.Add(new { Id = "C", Descripcion = "Casado" });
            EstadoCivil.Add(new { Id = "D", Descripcion = "Divorciado" });
            EstadoCivil.Add(new { Id = "S", Descripcion = "Soltero" });
            EstadoCivil.Add(new { Id = "U", Descripcion = "Union Libres" });
            EstadoCivil.Add(new { Id = "V", Descripcion = "Viudo" });

            ViewBag.per_EstadoCivil = new SelectList(EstadoCivil, "Id", "Descripcion");
            ViewBag.per_Sexo = new SelectList(Sexo, "Id", "Descripcion");

            ViewBag.per_UsuarioCrea = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            ViewBag.per_UsuarioModifica = new SelectList(db.tbUsuarios, "usu_Id", "usu_NombreDeUsuario");
            return View();
        }
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
        [HttpPost]
        public ActionResult Create(tbPersonas tbPersonas)
        {
            string msj = "";

                var usuario = (int)Session["Id"];
                try
                {
                    var list = db.UDP_Persona_tbPersonas_Insert(tbPersonas.per_Identidad
                                                                ,tbPersonas.per_Nombres
                                                                ,tbPersonas.per_Apellidos
                                                                ,tbPersonas.per_FechaNacimiento
                                                                ,tbPersonas.per_Sexo
                                                                ,tbPersonas.per_Telefono
                                                                ,tbPersonas.per_CorreoElectronico
                                                                ,tbPersonas.per_EstadoCivil
                                                                , usuario
                                                                , Function.DatetimeNow());
                
                    foreach (UDP_Persona_tbPersonas_Insert_Result item in list)
                    {
                        msj = item.MensajeError + " ";
                    }
                }
                catch (Exception ex)
                {
                    msj = "-2";
                    ex.Message.ToString();
                }
            return RedirectToAction("Index");
        }
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
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
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
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
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
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
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPersonas tbPersonas = db.tbPersonas.Find(id);
            db.tbPersonas.Remove(tbPersonas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //*♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦♦*//
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
