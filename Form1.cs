using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "LibrarySystem";
            
            // Call the connection check as soon as the app starts
            CheckDatabaseConnection();
        }

        private void CheckDatabaseConnection()
        {
            try
            {
                // Attempt to connect to the database
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();
                    
                    // If we get here, the connection was successful
                    tbox.Text = "Connected to SQL Server";
                    tbox.BackColor = Color.LightGreen;
                }
            }
            catch (SqlException ex)
            {
                // If it fails, show error status
                tbox.Text = "SQL Connection Failed .\r\nCannot access rest of operations";
                tbox.BackColor = Color.LightCoral;

                // Lock down the navigation buttons
                btnManageBooksPage.Enabled = false;
                btnManageMembersPage.Enabled = false;
                btnMainDeskPage.Enabled = false;
                btnAdminPanelPage.Enabled = false;

                // Show an error pop-up with the exact SQL error
                MessageBox.Show("Could not connect to the database. The system is locked.\n\nError Details:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnManageBooksPage_Click_1(object sender, EventArgs e)
        {
            BookManagerForm bookForm = new BookManagerForm();
            bookForm.Show();
            this.Hide();
        }
        
        private void btnAdminPanelPage_Click(object sender, EventArgs e)
        {
            AdminPanelForm adminpanel = new AdminPanelForm();
            adminpanel.Show();
            this.Hide();
        }

        private void btnManageMembersPage_Click(object sender, EventArgs e)
        {
            MemberDirectoryForm memberForm = new MemberDirectoryForm();
            memberForm.Show();
            this.Hide();
        }

        private void btnMainDeskPage_Click(object sender, EventArgs e)
        {
            MainDeskForm maindesk = new MainDeskForm();
            maindesk.Show();
            this.Hide();
        }
    }
}