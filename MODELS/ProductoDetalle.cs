using System.ComponentModel.DataAnnotations;

public class ProductoDetalle{

    #nullable disable
    [Key]
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string Descripcion { get; set; }
    public float Cantidad { get; set; }
    public float Precio { get; set; }
    public Productos producto { get; set; } = new Productos();
    public ProductoDetalle(){}

    public ProductoDetalle(string descripcion, float cantidad, float precio)
    {
        
        this.Descripcion = descripcion;
        this.Cantidad = cantidad;
        this.Precio = precio;
    }

}