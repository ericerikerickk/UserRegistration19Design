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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Desktop\\UserRegistration19\\UserRegistration19\\LoginDB.mdf;Integrated Security=True");
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string Password = "";
            bool IsExist = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblUserRegistration where UserName='" + txtUserName.Text + "'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Password = sdr.GetString(2);  //get the user password from db if the user name is exist in that.  
                IsExist = true;
            }
            con.Close();
            if (IsExist)  //if record exis in db , it will return true, otherwise it will return false  
            {
                if (Cryptography.Decrypt(Password).Equals(txtPassword.Text))
                {
                    UserSession.Id = GetUserID(username);
                    Form4 frm4 = new Form4();
                    frm4.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Password is wrong!...", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else  //showing the error message if user credential is wrong  
            {
                MessageBox.Show("Please enter the valid credentials", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form1 register = new Form1();
            this.Hide();
            register.Show();
        }
        private int GetUserID(string username)
        {
            // Query database to get user ID based on username
            // ...
            int userID = 0;


            string sql = "SELECT Id FROM tblUserRegistration WHERE UserName = @Username";
            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@Username", username);

            try
            {
                // Open SQL connection
                con.Open();

                // Execute SQL command and get user ID
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userID = Convert.ToInt32(reader["Id"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle SQL exception
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                con.Close();
            }

            return userID;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Logout", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
