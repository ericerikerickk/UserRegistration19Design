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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Source\\Repos\\UserRegistration19\\LoginDB.mdf;Integrated Security=True");

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            con.Open();
            if(txtUserName.Text != "" && txtFname.Text != "" && txtLname.Text != "" && txtAddress.Text != "" && txtPhoneNumber.Text != "")
            {
                string username = txtUserName.Text;
                string fname = txtFname.Text;
                string lname = txtLname.Text;
                string address = txtAddress.Text;
                string phone = txtPhoneNumber.Text;
                    con.Close();
                    con.Open();
                    SqlCommand insert = new SqlCommand("insert into Users(username,fname,lname,address,phone)values('" + username + "','" + fname + "','" + lname + "','" + address + "','" + phone + "')", con);
                    insert.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("Please fill all the fields!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);  //showing the error message if any fields is empty  
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Form4 dashboard = new Form4();
            dashboard.Show();
            this.Hide();
        }
    }
}
