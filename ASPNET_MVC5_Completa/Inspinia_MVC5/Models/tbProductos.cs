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
    
    public partial class tbProductos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbProductos()
        {
            this.tbPrecios = new HashSet<tbPrecios>();
        }
    
        public int prod_Id { get; set; }
        public int cprod_Id { get; set; }
        public string prod_Descripcion { get; set; }
        public string prod_Codigo { get; set; }
        public bool prod_Estado { get; set; }
        public string prod_RazonInactivo { get; set; }
        public int prod_UsuarioCrea { get; set; }
        public System.DateTime prod_FechaCrea { get; set; }
        public Nullable<int> prod_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prod_FechaModifica { get; set; }
    
        public virtual tbCatalogoProductos tbCatalogoProductos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPrecios> tbPrecios { get; set; }
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
    }
}