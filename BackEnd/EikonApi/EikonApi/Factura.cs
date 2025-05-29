using System.ComponentModel.DataAnnotations;

namespace EikonApi
{
    public class Factura
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }


        /*
         * 
         * una Fecha (DateTime), un Cliente (string, nombre del cliente) 
         * */

        public class CrearActualizarFactura()
        {
            //Actualizar - Update
            //Anotaciones de datos, validar campos
            [Required(ErrorMessage = "El campo {0} no es correcto")]
            public string Numero { get; set; }

            [Required(ErrorMessage = "El campo {0} no es correcto")]
 
            public string Fecha { get; set; }

            [Required(ErrorMessage = "El campo {0} no es correcto")]
            public string Cliente { get; set; }

            public List<CrearActualizarFacturaDetalle> Detalle { get; set; }
        }
    }
}
