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
    
    public partial class tbPedidos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPedidos()
        {
            this.tbPedidosDetalle = new HashSet<tbPedidosDetalle>();
            this.SolicitudesApartadas = new HashSet<SolicitudesApartadas>();
        }
    
        public int pdidos_Id { get; set; }
        public Nullable<int> per_Id { get; set; }
        public string pdidos_estado { get; set; }
        public Nullable<System.DateTime> pdidos_Fecha { get; set; }
        public string pdidos_RazonInactivao { get; set; }
        public string pdidos_ubicacion { get; set; }
        public string pdidos_Lat { get; set; }
        public string pdidos_Lng { get; set; }
        public decimal pdidos_Total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPedidosDetalle> tbPedidosDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesApartadas> SolicitudesApartadas { get; set; }
        public virtual tbPersonas tbPersonas { get; set; }
    }
}
