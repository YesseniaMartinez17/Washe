using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    [MetadataType(typeof(cCategoriasServicios))]
    public partial class tbCategoriaServicios
    {
        //[Display(Name = "Uusario quien lo inserto")]
        //public string usu_crea { get; set; }

        //[Display(Name = "Usuario que lo modifico")]
        //public string usu_modifica { get; set; }
    }

    public class cCategoriasServicios
    {
        [Display(Name = "Id")]
        public int cserv_Id { get; set; }

        [Display(Name = "Descripción")]
        public string cserv_Descripcion { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> cserv_Estado { get; set; }

        [Display(Name = "Razón inactivo")]
        public string cserv_RazonInactivo { get; set; }

        [Display(Name = "Insertado por")]
        public int cserv_UsuarioCrea { get; set; }

        [Display(Name = "Fecha inserción")]
        public System.DateTime cserv_FechaCrea { get; set; }

        [Display(Name = "Usuario modifica")]
        public Nullable<int> cserv_UsuarioModifica { get; set; }

        [Display(Name = "Fecha modifica")]
        public Nullable<System.DateTime> cserv_FechaModifica { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbServicios> tbServicios { get; set; }
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
    }
}