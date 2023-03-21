using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext{

    #nullable disable
    public DbSet<Productos> producto { get; set; }

    public Contexto (DbContextOptions<Contexto> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Productos>().HasData(
            
            new Productos{
                ProductoId = 1,
                Descripcion ="Mani",
                Costo = 200,
                Precio = 250,
                Existencia = 10
            },
            new Productos{
                ProductoId = 2,
                Descripcion ="Pasas",
                Costo = 150,
                Precio = 100,
                Existencia = 5
            },
            new Productos{
                ProductoId = 3,
                Descripcion ="Ciruelas",
                Costo = 75,
                Precio = 300,
                Existencia = 10
            }
        );
    }
}