using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    [MetadataType(typeof(cPersonasUsuarios))]
    public class cPersonasUsuarios
    {
        public int usu_Id { get; set; }
        public string usu_NombreDeUsuario { get; set; }
        public string usu_Contrasenia { get; set; }
        public bool usu_BloqueoActivo { get; set; }
        public Nullable<int> rol_id { get; set; }
        public Nullable<int> per_Id { get; set; }
        public byte[] usu_Fotografia { get; set; }
    }
}