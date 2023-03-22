using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Productos{

    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "Es obligatorio introducir la descripcion")]
    public string? Descripcion { get; set; }
    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Campo obligatorio. Se debe indicar el costo del producto")]
    public float Costo { get; set; }
    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Campo obligatorio. Se debe indicar el precio")]
    public float Precio { get; set; }
    [Required(ErrorMessage = "Es obligatorio introducir la existencia del producto")]
    public float Existencia { get; set; }
    public Productos() { Descripcion = string.Empty; }

    [ForeignKey("ProductoId")]
    public virtual List<ProductoDetalle> ProductoDetalle { get; set; } = new List<ProductoDetalle>(); 

}