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

namespace WMS
{
    public partial class updateSupplier : Form
    {
        private SqlConnection connection;
        public updateSupplier()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\kashf\\Downloads\\WMS\\WMS\\WMS\\WMS\\WMS\\Database.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string id = SKUtb.Text;

            // Start with a base query
            string query = "SELECT ID FROM Suppliers WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(id))
            {
                query += " AND ID = @id";
            }

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                }

                int rowCount = (int)cmd.ExecuteScalar(); // Use ExecuteScalar to get the count of matching rows

                if (rowCount > 0)
                {
                    
                    update2Supplier up = new update2Supplier(id);
                    DialogResult result = up.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    // Product does not exist, show an error message
                    MessageBox.Show("Error: Product does not exist!");
                }
            }
        }
    }
}
