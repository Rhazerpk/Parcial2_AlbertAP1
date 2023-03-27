using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductoEmpacados{

    [Key]
    public int EmpacadosId {get; set;}

    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Seleccione un producto")]
    public int ProductoId {get; set;}
    public DateTime Fecha {get; set;} = DateTime.Now;

    [Required(ErrorMessage = "Campo obligatorio. Se debe indicar el concepto.")]
    public string? Concepto { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Campo obligatorio. Ingrese la cantidad")]
    public int Cantidad {get; set;} 

    [ForeignKey("EmpacadosId")]
    public virtual List<EmpacadosDetalle> EmpacadosDetalle { get; set; } = new List<EmpacadosDetalle>();

}