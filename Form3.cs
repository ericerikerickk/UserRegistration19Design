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
namespace UserRegistration19
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Source\\Repos\\UserRegistration19\\LoginDB.mdf;Integrated Security=True");
        private void loadDataGrid()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from book order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("Insert into book values ('" + txtno.Text + "', '" + txttitle.Text + "', '" + txtauthor.Text + "')", con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();
            loadDataGrid();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from book where title like '%" + txtSearch.Text + "%'", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = dataGridView1.Rows[e.RowIndex].Cells["accession_number"].Value.ToString();
            txttitle.Text = dataGridView1.Rows[e.RowIndex].Cells["title"].Value.ToString();
            txtauthor.Text = dataGridView1.Rows[e.RowIndex].Cells["author"].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();
            string no;
            no = txtno.Text;

            SqlCommand cmd = new SqlCommand("Update book SET title= '" + txttitle.Text + "', author='" + txtauthor.Text + "' where accession_number= '" + no + "'", con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully Updated!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();
            loadDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            int num = (int.Parse(txtno.Text));

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("Delete from book where accession_number= '" + num + "'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cancelled!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            loadDataGrid();
        }
    }
}
