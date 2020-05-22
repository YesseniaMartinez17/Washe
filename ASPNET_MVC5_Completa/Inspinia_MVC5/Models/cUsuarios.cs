using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    [MetadataType(typeof(cUsuarios))]
    public partial class tbUsuarios
    {

    }
    public class cUsuarios
    {
        public int usu_Id { get; set; }
        [Display(Name="Usuario")]
        public string usu_NombreDeUsuario { get; set; }
        [Display(Name = "Clave")]
        public string usu_Contrasenia { get; set; }
        public Nullable<int> rol_id { get; set; }
        public Nullable<int> per_Id { get; set; }
        [Display(Name = "Imagen")]
        public byte[] usu_Fotografia { get; set; }
    }
}