using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ProductoBLL
{   

    #nullable disable
    private Contexto _contexto;

    public ProductoBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int id) 
    {
        
        bool encontrado = false;

        try
        {
            encontrado = _contexto.producto
            .Any(p => p.ProductoId == id);
        }
        catch (Exception)
        {
            throw;
        }

        return encontrado;
    }

    public bool Guardar(Productos producto) 
    {  
        
        if (!Existe(producto.ProductoId))
        {
        return  Insertar(producto);
        }
        else
        {
        return Modificar(producto);
        }
    }


    private bool Modificar(Productos producto)
    {

        bool paso = false;

        try
        {
            _contexto.Database.ExecuteSqlRaw($"DELETE FROM ProductoDetalle WHERE ProductoId={producto.ProductoId}");
            foreach (var Anterior in producto.ProductoDetalle)
            {
                _contexto.Entry(Anterior).State = EntityState.Added;
            }
            _contexto.Entry(producto).State = EntityState.Modified;
            paso = _contexto.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }
        return paso;
    }

    private bool Insertar(Productos producto) 
    {
        
        bool paso = false;

        try{
                _contexto.producto.Add(producto);
                paso = _contexto.SaveChanges() > 0;

        } catch(Exception)
        {
            throw;
        }
        return paso;
    }

    public Productos Buscar(int id) 
    {
        Productos producto;

        try
        {           
            producto = _contexto.producto
            .Include(x => x.ProductoDetalle)
            .Where(p => p.ProductoId == id)
            .AsNoTracking()
            .SingleOrDefault();
        }
        catch(Exception)
        {
            throw;
        }
        return producto;
    }

    public bool Eliminar(int Id) 
    {
        bool paso = false;

        try
        {
            var producto = _contexto.producto.Find(Id);

            if (producto != null)
            {
                _contexto.producto.Remove(producto);
                paso = _contexto.SaveChanges() > 0;
            }
            
        }
        catch (Exception)
        {
            throw;
        }

        return paso;
    }


    public List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
    {
        List<Productos> lista = new List<Productos>(); 

        try
        {
            lista = _contexto.producto
            .Include(x => x.ProductoDetalle)
            .Where(criterio)
            .AsNoTracking().ToList();

        }
        catch (Exception)
        {
            throw;
        }
        return lista;
    }
    public List<Productos> GetLista()
    {
        return _contexto.producto.ToList();
    }

    
}
    
