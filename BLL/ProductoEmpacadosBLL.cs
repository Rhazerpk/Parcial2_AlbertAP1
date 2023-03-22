using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ProductoEmpacadosBLL{

    #nullable disable
    private Contexto _contexto;

    public ProductoEmpacadosBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int id) 
    {
        bool existe = false;

        try
        {
            existe = _contexto.Empacados
            .Any(e => e.EmpacadosId == id);

        }
        catch (Exception)
        {
            throw;
        }
        return existe;
    }

    public ProductoEmpacados Buscar(int id) 
    {
        ProductoEmpacados entradaEmpacados;

        try
        {
            entradaEmpacados = _contexto.Empacados
            .Include( e => e.EmpacadosDetalle)
            .Where( e => e.EmpacadosId == id).Include( x => x.EmpacadosDetalle)
            .ThenInclude( x => x.producto).ThenInclude( x => x.ProductoDetalle)
            .AsNoTracking().SingleOrDefault();
        }
        catch (Exception)
        {
            throw;
        }

        return entradaEmpacados;

    }

    public bool Insertar(ProductoEmpacados productoEmpacado)
    {
        
        bool paso = false;

        try
        {
            
            foreach (var item in productoEmpacado.EmpacadosDetalle) 
            {
                _contexto.Entry(item).State = EntityState.Added;
                _contexto.Entry(item.producto).State = EntityState.Modified;

                item.producto.Existencia -= productoEmpacado.CantidadUtilizada;
            }

            var itemm = _contexto.producto.Find(productoEmpacado.ProductoId); 
            
            if(itemm != null)
            {
                
                itemm.Existencia += productoEmpacado.CantidadProducida;
                
            }
                _contexto.Empacados.Add(productoEmpacado);
                paso = _contexto.SaveChanges() > 0;

        }
        catch (Exception)
        {
            throw;
        }
        
        return paso;
    }

    public bool Guardar(ProductoEmpacados productoEmpacado)
    {         
        if (!Existe(productoEmpacado.EmpacadosId))
        {
            return  Insertar(productoEmpacado);
        }
        else
        {
            return Modificar(productoEmpacado);
        }
    }
    public bool Modificar(ProductoEmpacados productoEmpacado) 
    {
        bool paso = false;

        try
        {
            var lista = _contexto.Empacados
            .Where(x => x.EmpacadosId == productoEmpacado.EmpacadosId)
            .Include(x => x.EmpacadosDetalle)
            .ThenInclude(x => x.producto)
            .AsNoTracking()
            .SingleOrDefault();

        if(lista != null)
        {

            foreach (var item in lista.EmpacadosDetalle)
            {
                item.producto.Existencia += productoEmpacado.CantidadUtilizada; 
            }
                
            var _item = _contexto.producto.Find(productoEmpacado.ProductoId);

            if(_item != null)
            {

                _item.Existencia -= productoEmpacado.CantidadProducida; 

            }

        _contexto.Database.ExecuteSqlRaw($"Delete FROM EmpacadosDetalle where EmpacadosId={productoEmpacado.EmpacadosId}");

        foreach (var item in productoEmpacado.EmpacadosDetalle)
        {
            
            _contexto.Entry(item).State = EntityState.Added;
            _contexto.Entry(item.producto).State = EntityState.Modified;

            item.producto.Existencia -= productoEmpacado.CantidadUtilizada;

        }

        var producido = _contexto.producto.Find(productoEmpacado.ProductoId);

        if(producido != null)
        {
            producido.Existencia += productoEmpacado.CantidadProducida; 
            
        }

            _contexto.Entry(productoEmpacado).State = EntityState.Modified;
            paso = _contexto.SaveChanges() > 0;
        }

        }
        catch (Exception)
        {
            throw;
        }
        return paso;

    }


    public bool Eliminar(int id) 
    {
        bool paso = false;

        try
        {
        var entradaEmpacado = _contexto.Empacados.Find(id);

        if (entradaEmpacado != null)
        {      

            var item = _contexto.producto.Find(entradaEmpacado.ProductoId);
            if(item != null)
            {

                item.Existencia -= entradaEmpacado.CantidadProducida;

            }

            foreach (var itemm in entradaEmpacado.EmpacadosDetalle)
            {
                _contexto.Entry(itemm.entradaEmpacado).State = EntityState.Modified;
                _contexto.Entry(itemm.producto).State = EntityState.Modified;
                itemm.producto.Existencia += entradaEmpacado.CantidadUtilizada;
            }

                _contexto.Empacados.Remove(entradaEmpacado);
                paso = _contexto.SaveChanges() > 0;

        }

        }

        catch (Exception)
        {
            throw;
        }
        return paso;
    }

    public List<ProductoEmpacados> GetList(Expression<Func<ProductoEmpacados, bool>> criterio)
    {
        List<ProductoEmpacados> lista = new List<ProductoEmpacados>(); 
        try
        {
            lista = _contexto.Empacados

            .Include(x => x.EmpacadosDetalle)
            .ThenInclude(x => x.producto)
            .ThenInclude(x => x.ProductoDetalle)
            .Where(criterio)
            .AsNoTracking()
            .ToList();

        }
        catch (Exception)
        {
            throw;
        }

        return lista;
    }      
           
}