//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SolicitudesApartadas
    {
        public int sol_Id { get; set; }
        public Nullable<System.DateTime> sol_Fecha { get; set; }
        public Nullable<int> per_Id { get; set; }
        public Nullable<int> pdido_Id { get; set; }
        public string sol_Estado { get; set; }
        public Nullable<int> usu_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> usu_FechaModifica { get; set; }
    
        public virtual tbPedidos tbPedidos { get; set; }
        public virtual tbPersonas tbPersonas { get; set; }
        public virtual tbUsuarios tbUsuarios { get; set; }
    }
}