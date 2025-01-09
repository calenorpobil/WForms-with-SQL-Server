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
        string connectionString = "Data Source =DESKTOP-NNKTF0L\\SQLEXPRESS;Initial Catalog=BBDD9Eval;Integrated Security = True";
        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void cargar()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM album";

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

        private void btAñadir_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO album (id_album, NOMBRE, " +
                    "num_pistas, fecha_salida, artista_creador, " +
                    "discografica_propietaria) VALUES (@id_album, @NOMBRE, " +
                    "@num_pistas, @fecha_salida, @artista_creador, " +
                    "@discografica_propietaria)";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    DateTime fecha_salida = new DateTime(2023, 1, 1);
                    String input = "";
                    input = tbTextBox.Text;
                    int id_album = int.Parse(input);
                    command.Parameters.AddWithValue("@id_album", input);
                    command.Parameters.AddWithValue("@NOMBRE", "Juan");
                    command.Parameters.AddWithValue("@fecha_salida", fecha_salida);
                    command.Parameters.AddWithValue("@num_pistas", 50000);
                    command.Parameters.AddWithValue("@artista_creador", 1);
                    command.Parameters.AddWithValue("@discografica_propietaria", 3);

                    command.ExecuteNonQuery();
                }
            }

            cargar();
        }
    }
}
