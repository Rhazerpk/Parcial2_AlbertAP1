using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EmpacadosDetalle{

    [Key]
    public int EmpacadosDetalleId {get; set;}
    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Campo obligatorio. Ingrese la cantidad")]
    public float Cantidad { get; set; }
    public int EmpacadosId {get; set;}

    [ForeignKey("ProductoId")] 
    public Productos producto { get; set; } = new Productos();

    public ProductoEmpacados entradaEmpacado { get; set; } = new ProductoEmpacados();
}