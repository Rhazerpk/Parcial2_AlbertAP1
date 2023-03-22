using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext{

    #nullable disable
    public DbSet<Productos> producto { get; set; }
    public DbSet<ProductoEmpacados> Empacados { get; set; }

    public Contexto (DbContextOptions<Contexto> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Productos>().HasData(
            
            new Productos{
                ProductoId = 1,
                Descripcion ="Maní",
                Costo = 75,
                Precio = 100,
                Existencia = 40
            },
            new Productos{
                ProductoId = 2,
                Descripcion ="Pistachos",
                Costo = 80,
                Precio = 105,
                Existencia = 70
            },
            new Productos{
                ProductoId = 3,
                Descripcion ="Ciruelas",
                Costo = 90,
                Precio = 125,
                Existencia = 30
            },
            new Productos{
                ProductoId = 4,
                Descripcion ="Arándanos",
                Costo = 125,
                Precio = 175,
                Existencia = 50
            },
            new Productos{
                ProductoId = 5,
                Descripcion ="Mixta",
                Costo = 175,
                Precio = 300,
                Existencia = 100
            }
        );
    }
}