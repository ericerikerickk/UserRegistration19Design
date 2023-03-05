using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserRegistration19
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            int userID = UserSession.Id;
            InitializeComponent();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            // Clear user session and show login form
            UserSession.Id = 0;
            Form2 loginForm = new Form2();
            loginForm.Show();
            this.Hide();
        }

        private void borrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 borrower = new Form5();
            borrower.Show();
            this.Hide();
        }
    }
}
