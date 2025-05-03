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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NProducto nProducto = new NProducto();

        public MainWindow()
        {
            InitializeComponent();
            ListarProductos();
        }

        private void ListarProductos()
        {
            // Obtener todos los productos de la capa de negocio
            var productos = nProducto.ListarProductosPorNombre(""); // Pasamos un string vacío para obtener todos

            // Asignar los productos al DataGrid para que se muestren en la interfaz
            ProductsDataGrid.ItemsSource = productos;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string nombreProducto = SearchTextBox.Text;

            // Obtener los productos filtrados por nombre desde la capa de negocio
            var productos = nProducto.ListarProductosPorNombre(nombreProducto);

            // Asignar los productos filtrados al DataGrid
            ProductsDataGrid.ItemsSource = productos;
        }
    }
}