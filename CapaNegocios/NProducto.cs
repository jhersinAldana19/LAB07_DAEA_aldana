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
        private readonly DatosProductos dProducto = new DatosProductos();

        // Listar todos los productos
        public List<Product> ListarProductos()
        {
            return dProducto.ObtenerProductos();
        }

        // Buscar productos por nombre (filtro en memoria)
        public List<Product> ListarProductosPorNombre(string nombre)
        {
            var productos = dProducto.ObtenerProductos();

            if (string.IsNullOrWhiteSpace(nombre))
                return productos;

            return productos
                .Where(p => p.Name.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Registrar un nuevo producto
        public void Registrar(Product producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            dProducto.RegistrarProducto(producto);
        }

        // Actualizar un producto existente
        public void Actualizar(Product producto)
        {
            if (producto == null || producto.ProductId <= 0)
                throw new ArgumentException("Producto no válido para actualizar.");

            dProducto.ActualizarProducto(producto);
        }

        // Eliminar un producto por ID (eliminación lógica)
        public void Eliminar(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID de producto no válido.");

            dProducto.EliminarProducto(productId);
        }
    }
}