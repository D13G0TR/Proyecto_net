using System;
using System.Linq;
using System.Windows.Forms;
using Prueba2_.Net_Proyecto.Models;

namespace Prueba2_.Net_Proyecto
{
    public partial class Form1 : Form
    {
        private InventarioContext _context;

        public Form1()
        {
            InitializeComponent();
            InitializeDbContext();
            LoadProductos();
        }

        private void InitializeDbContext()
        {
            _context = new InventarioContext();
        }

        private void LoadProductos()
        {
            dgvProductos.DataSource = _context.productos.ToList();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            if (int.TryParse(txtPrecio.Text.Trim(), out int precio))
            {
                productos nuevoProducto = new productos { nombre = nombre, descripcion = descripcion, precio = precio, creacion = DateTime.Now };
                _context.productos.Add(nuevoProducto);
                _context.SaveChanges();
                MessageBox.Show("Producto agregado correctamente.");
                LimpiarCampos();
                LoadProductos();
            }
            else
            {
                MessageBox.Show("El precio debe ser un número válido.");
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["idProducto"].Value);
                productos producto = _context.productos.Find(id);
                if (producto != null)
                {
                    producto.nombre = txtNombre.Text.Trim();
                    producto.descripcion = txtDescripcion.Text.Trim();
                    if (int.TryParse(txtPrecio.Text.Trim(), out int precio))
                    {
                        producto.precio = precio;
                        _context.SaveChanges();
                        MessageBox.Show("Producto actualizado correctamente.");
                        LimpiarCampos();
                        LoadProductos();
                    }
                    else
                    {
                        MessageBox.Show("El precio debe ser un número válido.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto para actualizar.");
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["idProducto"].Value);
                    productos producto = _context.productos.Find(id);
                    if (producto != null)
                    {
                        _context.productos.Remove(producto);
                        _context.SaveChanges();
                        MessageBox.Show("Producto eliminado correctamente.");
                        LoadProductos();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto para eliminar.");
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
        }

        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtDescripcion.Text = row.Cells["descripcion"].Value.ToString();
                txtPrecio.Text = row.Cells["precio"].Value.ToString();
            }
        }
    }
}
