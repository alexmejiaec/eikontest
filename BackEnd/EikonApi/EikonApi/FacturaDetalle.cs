using System.ComponentModel.DataAnnotations;

namespace EikonApi
{
    public class FacturaDetalle
    {
        [Key]
        public int IdDet { get; set; }
        public int IdCab { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        /*   y una lista de Ítems(cada ítem con Descripción (string) y Precio (decimal)).
         *   */
    }

    public class CrearActualizarFacturaDetalle()
    {

        //Actualizar - Update
        //Anotaciones de datos, validar campos
        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(15, ErrorMessage = "El campo {0} debe tener al menos {1} letras")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        public decimal Precio { get; set; }

 
    }
}
