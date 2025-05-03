using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NProducto
    {
        DatosProductos dProducto = new DatosProductos();

        public List<Product> ListarProductosPorNombre(string nombre)
        {
            var productos = dProducto.ObtenerProductos();
            var filtro = nombre == "" ? productos :
                productos.Where(x => x.Name.Equals(nombre)).ToList();
            return filtro;
        }




    }
}
