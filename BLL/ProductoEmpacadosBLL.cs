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

    public async Task<bool> Insertar(ProductoEmpacados productoEmpacado)
    {
        
        bool paso = false;

        try
        {
            
            foreach (var item in productoEmpacado.EmpacadosDetalle) 
            {
                _contexto.Entry(item).State = EntityState.Added;
                _contexto.Entry(item.producto).State = EntityState.Modified;

                item.producto.Existencia -= item.Cantidad;
            }

            var itemm = _contexto.producto.Find(productoEmpacado.ProductoId); 
            
            if(itemm != null)
            {
                
                itemm.Existencia += productoEmpacado.Cantidad;
                
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

    public async Task<bool> Guardar(ProductoEmpacados productoEmpacado)
    {         
        if (!Existe(productoEmpacado.EmpacadosId))
        {
            return  await Insertar(productoEmpacado);
        }
        else
        {
            return await Modificar(productoEmpacado);
        }
    }
    public async Task<bool> Modificar(ProductoEmpacados productoEmpacado) 
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
                item.producto.Existencia += item.Cantidad; 
            }
                
            var _item = _contexto.producto.Find(productoEmpacado.ProductoId);

            if(_item != null)
            {

                _item.Existencia -= productoEmpacado.Cantidad; 

            }

        _contexto.Database.ExecuteSqlRaw($"Delete FROM EmpacadosDetalle where EmpacadosId={productoEmpacado.EmpacadosId}");

        foreach (var item in productoEmpacado.EmpacadosDetalle)
        {
            
            _contexto.Entry(item).State = EntityState.Added;
            _contexto.Entry(item.producto).State = EntityState.Modified;

            item.producto.Existencia -= item.Cantidad;

        }

        var producido = _contexto.producto.Find(productoEmpacado.ProductoId);

        if(producido != null)
        {
            producido.Existencia += productoEmpacado.Cantidad; 
            
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

                item.Existencia -= entradaEmpacado.Cantidad;

            }

            foreach (var itemm in entradaEmpacado.EmpacadosDetalle)
            {
                _contexto.Entry(itemm.entradaEmpacado).State = EntityState.Modified;
                _contexto.Entry(itemm.producto).State = EntityState.Modified;

                itemm.producto.Existencia += itemm.Cantidad;
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