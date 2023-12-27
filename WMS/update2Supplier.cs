using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WMS
{
    public partial class update2Supplier : Form
    {
        static private string iD;
        public update2Supplier(string id)
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            iD = id;
        }
        private SqlConnection connection;
        private void InitializeDatabaseConnection()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\kashf\\Downloads\\WMS\\WMS\\WMS\\WMS\\WMS\\Database.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        private void update2Supplier_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q2 = "Select productType from Supplier where Id = @Id";
            SqlCommand command3 = new SqlCommand(q2, connection);
            command3.Parameters.AddWithValue("@Id", iD);
            // ExecuteScalar is used to retrieve a single value from the query result.
            object result = command3.ExecuteScalar();
            string names = result.ToString();
        
            string q = "Select SKU from Product where Name = @Name";
            SqlCommand command4 = new SqlCommand(q, connection);
            command4.Parameters.AddWithValue("@Name", names);
            object result2 = command4.ExecuteScalar();
            string id = result2.ToString();
            string name = nameee.Text;
            string unit = unitss.Text;
            string type = itemm.Text;
            string prices = priceee.Text;
            string num = nummm.Text;
            string method = pay.Text;
            string updateQuery = "UPDATE Suppliers SET " +
                                "Name = ISNULL(@Name, Name), " +
                                "phone = ISNULL(@phone, phone), " +
                                "payment = ISNULL(@payment, payment), " +
                                "units = ISNULL(@units, units), " +
                                "priceU = ISNULL(@priceU, priceU), " +
                                "productType = ISNULL(@productType, productType) " +
                                "WHERE Id = @Id";
            

            using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
            {
                cmd.Parameters.AddWithValue("@Id", iD);

                // Set parameters based on whether the fields are empty or not
                if (!string.IsNullOrWhiteSpace(name))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Name", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    cmd.Parameters.AddWithValue("@units", unit);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@units", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(type))
                {
                    cmd.Parameters.AddWithValue("@productType", type);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@productType", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(prices))
                {
                    cmd.Parameters.AddWithValue("@priceU", prices);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@priceU", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(num))
                {
                    cmd.Parameters.AddWithValue("@phone", num);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@phone", DBNull.Value);
                }
                if (!string.IsNullOrWhiteSpace(method))
                {
                    cmd.Parameters.AddWithValue("@payment", method);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@payment", DBNull.Value);
                }
                string query2 = "Update Product SET Name = @Name, Price =  @Price, StockLevel = @StockLevel where SKU = @SKU";
                SqlCommand command1 = new SqlCommand(query2, connection);
                command1.Parameters.AddWithValue("@Name", name);
                command1.Parameters.AddWithValue("@Price", prices);
                command1.Parameters.AddWithValue("@StockLevel", int.Parse(unit));
                command1.Parameters.AddWithValue("@SKU", int.Parse(id));
                int rowsAffected2 = command1.ExecuteNonQuery();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Supplier updated successfully.");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Error: Product updation failed.");
                }
            }
        }
    }
}
