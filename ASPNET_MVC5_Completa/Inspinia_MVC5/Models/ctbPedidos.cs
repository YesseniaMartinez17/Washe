using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    [MetadataType(typeof(ctbPedidos))]
    public partial class tbPedidos
    {
    }
    public class ctbPedidos
    {
        [Display(Name = "Id")]
        public int pdidos_Id { get; set; }

        [Display(Name = "Id Cliente")]
        public Nullable<int> per_Id { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> pdidos_estado { get; set; }

        [Display(Name = "Fecha y hora")]
        public Nullable<System.DateTime> pdidos_Fecha { get; set; }

        [Display(Name = "Motivo desactivo")]
        public string pdidos_RazonInactivao { get; set; }

        [Display(Name = "Lugar")]
        public string pdidos_ubicacion { get; set; }

        [Display(Name = "Lat")]
        public string pdidos_Lat { get; set; }

        [Display(Name = "Lng")]
        public string pdidos_Lng { get; set; }

        [Display(Name = "Precio total del pedido")]
        public decimal pdidos_Total { get; set; }
    }
}