using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Cooperation
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //SqlConnection conn = new SqlConnection(@"Data Source=NEWAN0805\SQLEXPRESS;Initial Catalog=""ABC Cooperation"";Integrated Security=True");
        private readonly string connectionString = @"Data Source=NEWAN0805\SQLEXPRESS;Initial Catalog=""ABC Cooperation"";Integrated Security=True";


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string quary = "SELECT * FROM login WHERE username='"+username+"' AND password='"+password+"'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(quary, conn);
                    SqlDataReader row = cmd.ExecuteReader();

                    if (row.HasRows)
                    {
                        this.Hide();
                        Registration reg = new Registration();
                        reg.Show();
                    }
                    else
                    {
                        MessageBox.Show("Insvalid Login Credentials, Please Check Username Or Password And Try Again!", "Invatlid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
