using project;
using System;
using System.Data;
using System.Windows.Forms;

namespace Userdata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvuserinfo.CellClick += dgvuserinfo_CellClick;

            DataTable dt = u.Select();
            dgvuserinfo.DataSource = dt;
        }
        userformclass u = new userformclass();
        private void add_Click(object sender, EventArgs e)

        {
              u.Name = txtName.Text;
              u.Email = txtEmail.Text;
            u.Gender = checkBox1.Checked ? "Male" : "Female";
            u.Mobile = txtMobile.Text;

            bool success = u.Insert(u);
            if (success)
            {
                MessageBox.Show("Successfully Added");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Invalid entry Try Again!");
            }
            DataTable dt = u.Select();
            dgvuserinfo.DataSource = dt;
           
        }


        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = u.Select();
            dgvuserinfo.DataSource = dt; 
        }
        private void ClearFields()
        {
            txtUserID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            txtMobile.Text = string.Empty;
        }


        private void update_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                userformclass u = new userformclass();
                u.UserID = txtUserID.Text;
                u.Name = txtName.Text;
                u.Email = txtEmail.Text;
                u.Gender = checkBox1.Checked ? "Male" : "Female";
                u.Mobile = txtMobile.Text;

                bool success = u.Update(u);
                if (success)
                {
                    MessageBox.Show("Successfully Updated");
                    ClearFields();
                    DataTable dt = u.Select();
                    dgvuserinfo.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Update failed! Try Again.");
                }
            }
            else
            {
                MessageBox.Show("Please select a record to update.");
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string userID = txtUserID.Text;
                    userformclass u = new userformclass();
                    bool success = u.Delete(userID);

                    if (success)
                    {
                        MessageBox.Show("Successfully Deleted");
                        ClearFields();
                        DataTable dt = u.Select();
                        dgvuserinfo.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Delete failed! Try Again.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.");
            }
        

             }

        private void clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvuserinfo_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            if (e.RowIndex >= 0)            
            {
                DataGridViewRow row = dgvuserinfo.Rows[e.RowIndex];

                txtUserID.Text = row.Cells["UserID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();

                string gender = row.Cells["Gender"].Value.ToString();
                checkBox1.Checked = (gender == "Male");
                checkBox2.Checked = (gender == "Female");

                txtMobile.Text = row.Cells["Mobile"].Value.ToString();
            }
        }
    }
}

