using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    [MetadataType(typeof(cPedidosDetalle))]
    public partial class tbPedidosDetalle
    {
        
    }
    public class cPedidosDetalle
    {
        [Display(Name = "Id detalle")]
        public int pdet_Id { get; set; }
        [Display(Name = "Id pedido")]
        public int pdidos_Id { get; set; }
        [Display(Name = "Id servicio")]
        public int serv_Id { get; set; }

        [Display(Name = "Precio")]
        public decimal pdet_PrecioActual { get; set; }
        [Display(Name = "Estado")]
        public Nullable<bool> pdet_Estado { get; set; }
        [Display(Name = "Razon inactivado")]
        public string pdet_RazonInactivo { get; set; }




        [Display(Name = "Pedido")]
        public virtual tbPedidos tbPedidos { get; set; }
        [Display(Name = "Servicio de lavado solicitado")]
        public virtual tbServicios tbServicios { get; set; }
    }
}