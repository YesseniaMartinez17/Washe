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
    
    public partial class tbPrecios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPrecios()
        {
            this.tbPrecios1 = new HashSet<tbPrecios>();
        }
    
        public int prc_Id { get; set; }
        public Nullable<int> prod_Id { get; set; }
        public decimal prc_Precio { get; set; }
        public Nullable<int> prc_PrecioAnterior { get; set; }
        public bool prc_Estado { get; set; }
        public string prc_RazonInactivo { get; set; }
        public int prc_UsuarioCrea { get; set; }
        public System.DateTime prc_FechaCrea { get; set; }
        public Nullable<int> prc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prc_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPrecios> tbPrecios1 { get; set; }
        public virtual tbPrecios tbPrecios2 { get; set; }
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        public virtual tbProductos tbProductos { get; set; }
    }
}