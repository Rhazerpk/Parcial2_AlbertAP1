using System.ComponentModel.DataAnnotations;

public class ProductoDetalle{

    #nullable disable
    [Key]
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string Presentacion { get; set; }
    public float Cantidad { get; set; }
    public float Precio { get; set; }
    public Productos producto { get; set; } = new Productos();
    public ProductoDetalle(){}

    public ProductoDetalle(string presentacion, float cantidad, float precio)
    {
        
        this.Presentacion = presentacion;
        this.Cantidad = cantidad;
        this.Precio = precio;
    }

}