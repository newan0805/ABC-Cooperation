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

namespace ABC_Cooperation
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        //SqlConnection conn = new SqlConnection(@"Data Source=NEWAN0805\SQLEXPRESS;Initial Catalog=""ABC Cooperation"";Integrated Security=True");
        private readonly string connectionString = @"Data Source=NEWAN0805\SQLEXPRESS;Initial Catalog=""ABC Cooperation"";Integrated Security=True";

        private void txtLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void txtExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure, do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }

            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbEmpNo.Text = "";

            txtFirstName.Text = "";
            txtLastName.Text = "";

            dateDOB.Format = DateTimePickerFormat.Custom;
            dateDOB.CustomFormat = "yyyy/MM/dd";
            DateTime today = DateTime.Today;
            dateDOB.Text = today.ToString();

            radioMale.Checked = false; 
            radioMale.Checked = false;

            txtAddress.Text = "";
            txtEmail.Text = "";

            txtMPhone.Text = "";
            txtHPhone.Text = "";

            txtDepName.Text = "";
            txtDesignation.Text = "";
            txtEmpType.Text = "";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string EmpNo = cmbEmpNo.Text;

                string Fname = txtFirstName.Text;
                string Lname = txtLastName.Text;

                dateDOB.Format = DateTimePickerFormat.Custom;
                dateDOB.CustomFormat = "yyyy/MM/dd";

                

                string gender;
                if (radioMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                string address = txtAddress.Text;
                string email = txtEmail.Text;

                int mPhone = int.Parse(txtMPhone.Text);
                int hPhone = int.Parse(txtHPhone.Text);

                string depName = txtDepName.Text;
                string designation = txtDesignation.Text;
                string empType = txtEmpType.Text;


                string quary = "INSERT INTO employee VALUES ('"+Fname+"', '"+Lname+"', '"+dateDOB.Text+"', '"+gender+"', '"+address+"', " +
                               "'"+email+"', "+mPhone+", "+hPhone+", '"+depName+"', '"+designation+"', '"+empType+"')";

                using (SqlConnection conn = new SqlConnection(connectionString))
                { 
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(quary, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Record added successfully!", "Register Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex) {
                string msg = "Insert Error: ";
                msg += ex.Message;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                string regNo = cmbEmpNo.Text;

                string quary = "DELETE FROM employee WHERE emptNo = '"+regNo+"'";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(quary, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string regNo = cmbEmpNo.Text;
                if (regNo != "New Register")
                {
                    string EmpNo = cmbEmpNo.Text;

                    string Fname = txtFirstName.Text;
                    string Lname = txtLastName.Text;

                    dateDOB.Format = DateTimePickerFormat.Custom;
                    dateDOB.CustomFormat = "yyyy/MM/dd";

                    string gender;
                    if (radioMale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }

                    string address = txtAddress.Text;
                    string email = txtEmail.Text;

                    int mPhone = int.Parse(txtMPhone.Text);
                    int hPhone = int.Parse(txtHPhone.Text);

                    string depName = txtDepName.Text;
                    string designation = txtDesignation.Text;
                    string empType = txtEmpType.Text;


                    string quary = "UPDATE employee SET firstName = '" + txtFirstName+"', lastName = '"+txtLastName+"', dateOfBirth = '"+dateDOB.Text+"', " +
                                   "getnder = '"+gender+"', address = '"+address+"', "+
                                   "email = '"+email+"', mobilePhone = "+mPhone+", homePhone = "+hPhone+", departmentName = '"+depName+"', "+
                                   "designation = '"+designation+"', employeeType = '"+empType+"' WHERE emptNo = "+regNo+")";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(quary, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Record updated successfully!", "Update Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                string msg = "Update Error: ";
                msg += ex.Message;
            }
        }

        private void cmbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           string regNo = cmbEmpNo.Text;
            if(regNo != "New Register")
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string quary = "SELECT * FROM employee WHERE emptNo = '" + regNo + "'";
                    SqlCommand cmd = new SqlCommand(quary, conn);
                    SqlDataReader row = cmd.ExecuteReader();

                    while (row.Read())
                    {
                        cmbEmpNo.Text = row[0].ToString();
                        txtFirstName.Text = row[1].ToString();
                        txtLastName.Text = row[2].ToString();

                        dateDOB.Format = DateTimePickerFormat.Custom;
                        dateDOB.CustomFormat = "yyyy/MM/dd";
                        dateDOB.Text = row[3].ToString();

                        if (row[4].ToString() == "Male")
                        {
                            radioMale.Checked = true;
                            radioFemale.Checked = false;
                        }
                        else
                        {
                            radioMale.Checked = false;
                            radioFemale.Checked = true;
                        }

                        txtAddress.Text = row[5].ToString();
                        txtEmail.Text = row[6].ToString();
                        txtMPhone.Text = row[7].ToString();
                        txtHPhone.Text = row[8].ToString();
                        txtDepName.Text = row[9].ToString();
                        txtDesignation.Text = row[10].ToString();
                        txtEmpType.Text = row[11].ToString();
                    }
                    conn.Close();
                }
                btnRegister.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                cmbEmpNo.Text = "";

                txtFirstName.Text = "";
                txtLastName.Text = "";

                dateDOB.Format = DateTimePickerFormat.Custom;
                dateDOB.CustomFormat = "yyyy/MM/dd";
                DateTime today = DateTime.Today;
                dateDOB.Text = today.ToString();

                radioMale.Checked = false;
                radioMale.Checked = false;

                txtAddress.Text = "";
                txtEmail.Text = "";

                txtMPhone.Text = "";
                txtHPhone.Text = "";

                txtDepName.Text = "";
                txtDesignation.Text = "";
                txtEmpType.Text = "";

                btnRegister.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        public void refresher()
        {
            dateDOB.Format = DateTimePickerFormat.Custom;
            dateDOB.CustomFormat = "yyyy/MM/dd";
            DateTime today = DateTime.Today;
            dateDOB.Text = today.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string quary = "SELECT * FROM employee";
                SqlCommand cmd = new SqlCommand(quary, conn);
                SqlDataReader row = cmd.ExecuteReader();
                if (!cmbEmpNo.Items.Contains("New Register"))
                {
                    cmbEmpNo.Items.Add("New Register");
                }
                else
                {
                    while (row.Read())
                    {
                        if (!cmbEmpNo.Items.Contains((row[0].ToString())))
                        {
                            cmbEmpNo.Items.Add(row[0].ToString());
                        }
                        //refresher();
                    }
                        conn.Close();
                }
                
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            refresher();
        }

        private void txtRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration_Load(sender, e);
        }
    }
}
