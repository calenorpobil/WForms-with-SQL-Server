using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBDDInterfaces
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //private string connectionString = "Server=localhost;Database=TestDB;Trusted_Connection=True;";
        string connectionString = "Data Source =DESKTOP-MDAC0QE\\SQLEXPRESS;Initial Catalog=Empleados;Integrated Security = True";
        private void Form1_Load(object sender, EventArgs e)
        {
            cargar("SELECT * FROM ciudades");

        }

        private void cargar(string selectQuery)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(selectQuery, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        var dataGridView = this.Controls["dataGridView"] as DataGridView;
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

        private void rellenar(string selectQuery, SqlConnection connection)
        {
        }

        private void btAñadir_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO ciudades (idCiudad , nomCiudad , " +
                    "nomProvincia) VALUES (@idCiudad , @nomCiudad , @nomProvincia)";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    String inputId, inputNombre = "";
                    inputId = tbId.Text;
                    inputNombre = tbNombre.Text;
                    //int
                    command.Parameters.AddWithValue("@idCiudad", inputId);
                    //varchar(15)
                    command.Parameters.AddWithValue("@nomCiudad", inputNombre);
                    //varchar(15)
                    command.Parameters.AddWithValue("@nomProvincia", "nom_provincia");
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        //lbNombre.Text = reader.GetSqlInt16(0)+"";
                    }
                    //command.ExecuteNonQuery();
                }
            }

            cargar("SELECT * FROM ciudades");
        }

        private void btBorrar_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "delete from ciudades where idCiudad = @id;";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    String input = "";
                    input = tbId.Text;
                    //int
                    command.Parameters.AddWithValue("@id", input);
                    command.ExecuteNonQuery();
                }
            }

            cargar("SELECT * FROM ciudades");
        }

        private void btModificar_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "update ciudades set nomCiudad = @nombre where idCiudad = @id;";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    String inputId, inputNombre = "";
                    inputId = tbId.Text;
                    inputNombre = tbNombre.Text;
                    //int
                    command.Parameters.AddWithValue("@id", inputId);
                    command.Parameters.AddWithValue("@nombre", inputNombre);
                    command.ExecuteNonQuery();
                }
            }

            cargar("SELECT * FROM ciudades");
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                string insertQuery = "select * from ciudades where nomCiudad = @nombre;";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    String inputBuscar = "";
                    inputBuscar = tbBuscar.Text;
                    //int
                    command.Parameters.AddWithValue("@nombre", inputBuscar);
                    command.ExecuteNonQuery();
                    
                }
            }
            cargar("select * from ciudades where nomCiudad = @nombre;");


        }
    }
}
