using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    [MetadataType(typeof(cPersonas))]
    public partial class tbPersonas
    {

    }
    public class cPersonas
    {
        [Display(Name = "Id")]
        public int per_Id { get; set; }

        [Display(Name = "Identidad")]
        public string per_Identidad { get; set; }

        [Display(Name = "Nombre")]
        public string per_Nombres { get; set; }

        [Display(Name = "Apellido")]
        public string per_Apellidos { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public Nullable<System.DateTime> per_FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        public string per_Sexo { get; set; }

        [Display(Name = "Telefono")]
        public string per_Telefono { get; set; }

        [Display(Name = "Email")]
        public string per_CorreoElectronico { get; set; }

        [Display(Name = "Estado civil")]
        public string per_EstadoCivil { get; set; }

        [Display(Name = "Estado")]
        public bool per_Estado { get; set; }

        [Display(Name = "Razon inactivo")]
        public string per_RazonInactivo { get; set; }

        [Display(Name = "Insertado por")]
        public int per_UsuarioCrea { get; set; }

        [Display(Name = "Fecha inserción")]
        public System.DateTime per_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> per_UsuarioModifica { get; set; }

        [Display(Name = "Fecha modificación")]
        public Nullable<System.DateTime> per_FechaModifica { get; set; }

        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
    }
}