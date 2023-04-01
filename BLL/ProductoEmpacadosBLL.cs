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
                
                item.producto.Existencia -= item.Cantidad;
                _contexto.Entry(item.producto).State = EntityState.Modified;
 
            }

            var itemm = _contexto.producto.Find(productoEmpacado.ProductoId); 
            
            if(itemm != null)
            {
                
                itemm.Existencia += productoEmpacado.Cantidad;
                
            }
                _contexto.Empacados.Add(productoEmpacado);
                paso = _contexto.SaveChanges() > 0;
                _contexto.Entry(productoEmpacado).State = EntityState.Detached;

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
            return Insertar(productoEmpacado);
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

            foreach(var item in lista.EmpacadosDetalle)
            {
                var producto = _contexto.producto.Find(item.EmpacadosDetalleId);

                if(producto != null)
                {
                    producto.Existencia += item.Cantidad;
                    _contexto.Entry(producto).State = EntityState.Modified;
                }
            }

            var producidoAntiguo = _contexto.producto.Find(lista.ProductoId);

            if(producidoAntiguo != null)
            {
                producidoAntiguo.Existencia -= lista.Cantidad;
                _contexto.Entry(producidoAntiguo).State = EntityState.Modified;
            }

        }
        
        _contexto.Database.ExecuteSqlRaw($"Delete FROM EmpacadosDetalle where EmpacadosId={productoEmpacado.EmpacadosId}");

        foreach(var nuevo in productoEmpacado.EmpacadosDetalle)
        {
            var producto = _contexto.producto.Find(nuevo.EmpacadosDetalleId);

            if(producto != null)
            {
                producto.Existencia -= nuevo.Cantidad;
                _contexto.Entry(nuevo).State = EntityState.Added;
            }
        }

        var _item = _contexto.producto.Find(productoEmpacado.ProductoId);

        if(_item != null)
        {

            _item.Existencia += productoEmpacado.Cantidad; 
            _contexto.Entry(_item).State = EntityState.Modified;

        }

        _contexto.Entry(productoEmpacado).State = EntityState.Modified;
        paso = _contexto.SaveChanges() > 0;
        _contexto.Entry(productoEmpacado).State = EntityState.Detached;
        
        }
        catch (Exception)
        {
            throw;
        }

        return paso;

    }


    public bool Eliminar(ProductoEmpacados empacados) 
    {
        bool paso = false;

        try
        {
        
        foreach(var item in empacados.EmpacadosDetalle)
        {
            var producto = _contexto.producto.Find(item.ProductoId);

            if(producto != null)
            {
                producto.Existencia += item.Cantidad;
                _contexto.Entry(producto).State = EntityState.Modified;
            }
        }

        var producido = _contexto.producto.Find(empacados.ProductoId);

        if (producido != null)
        {      
            producido.Existencia -= empacados.Cantidad;
            _contexto.Entry(producido).State = EntityState.Modified;
        }

        _contexto.Entry(empacados).State = EntityState.Deleted;
        paso = _contexto.SaveChanges() > 0;
        _contexto.Entry(empacados).State = EntityState.Detached;


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