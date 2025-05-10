using CapaEntidad;
using CapaNegocio;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace labCalificado_semana7
{
    public partial class MainWindow : Window
    {
        private NProducto nProducto = new NProducto();

        public MainWindow()
        {
            InitializeComponent();
            ListarProductos();
        }

        private void ListarProductos(string filtro = "")
        {
            var productos = nProducto.ListarProductosPorNombre(filtro);
            ProductsDataGrid.ItemsSource = productos;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string nombre = SearchTextBox.Text.Trim();
            ListarProductos(nombre);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos(out string error))
            {
                try
                {
                    Product nuevo = new Product
                    {
                        Name = NameTextBox.Text.Trim(),
                        Price = decimal.Parse(PriceTextBox.Text),
                        Stock = int.Parse(StockTextBox.Text),
                        Active = true
                    };

                    nProducto.Registrar(nuevo);
                    MessageBox.Show("Producto registrado con éxito.");
                    LimpiarCampos();
                    ListarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product productoSeleccionado)
            {
                if (ValidarCampos(out string error))
                {
                    try
                    {
                        productoSeleccionado.Name = NameTextBox.Text.Trim();
                        productoSeleccionado.Price = decimal.Parse(PriceTextBox.Text);
                        productoSeleccionado.Stock = int.Parse(StockTextBox.Text);

                        nProducto.Actualizar(productoSeleccionado);
                        MessageBox.Show("Producto actualizado correctamente.");
                        LimpiarCampos();
                        ListarProductos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un producto para actualizar.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product productoSeleccionado)
            {
                if (MessageBox.Show("¿Estás seguro de eliminar este producto?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        nProducto.Eliminar(productoSeleccionado.ProductId);
                        MessageBox.Show("Producto eliminado.");
                        LimpiarCampos();
                        ListarProductos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            ListarProductos();
        }

        private void ProductsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product producto)
            {
                NameTextBox.Text = producto.Name;
                PriceTextBox.Text = producto.Price.ToString();
                StockTextBox.Text = producto.Stock.ToString();
            }
        }

        private void LimpiarCampos()
        {
            NameTextBox.Text = "";
            PriceTextBox.Text = "";
            StockTextBox.Text = "";
            ProductsDataGrid.SelectedItem = null;
        }

        private bool ValidarCampos(out string mensaje)
        {
            mensaje = "";
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                mensaje = "El campo Nombre es obligatorio.";
            else if (!decimal.TryParse(PriceTextBox.Text, out _))
                mensaje = "El campo Precio debe ser un número válido.";
            else if (!int.TryParse(StockTextBox.Text, out _))
                mensaje = "El campo Stock debe ser un número entero válido.";

            return mensaje == "";
        }
    }
}