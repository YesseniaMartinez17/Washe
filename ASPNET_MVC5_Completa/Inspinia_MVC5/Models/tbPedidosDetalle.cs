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
    
    public partial class tbPedidosDetalle
    {
        public int pdet_Id { get; set; }
        public int pdidos_Id { get; set; }
        public int serv_Id { get; set; }
        public decimal pdet_PrecioActual { get; set; }
        public Nullable<bool> pdet_Estado { get; set; }
        public string pdet_RazonInactivo { get; set; }
    
        public virtual tbPedidos tbPedidos { get; set; }
        public virtual tbServicios tbServicios { get; set; }
    }
}