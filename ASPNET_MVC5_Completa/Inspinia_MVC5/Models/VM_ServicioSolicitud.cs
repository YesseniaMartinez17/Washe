using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{

    public partial class VM_ServicioSolicitud
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
    }
}