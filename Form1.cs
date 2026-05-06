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
                // Attempt to connect to the SQL Server
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();
                    
                    // Ask SQL Server which database is currently active
                    using (SqlCommand cmd = new SqlCommand("SELECT DB_NAME()", con))
                    {
                        string currentDb = cmd.ExecuteScalar()?.ToString();
                        
                        // Check if it is EXACTLY "LibrarySystem"
                        if (currentDb != null && currentDb.Equals("LibrarySystem", StringComparison.OrdinalIgnoreCase))
                        {
                            // Success!
                            tbox.Text = "Connected to LibrarySystem DB!";
                            tbox.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            // It connected to the server, but the database is wrong or missing
                            tbox.Text = $"Wrong DB: {currentDb}";
                            tbox.BackColor = Color.LightCoral;
                            
                            LockSystem($"Connected to SQL Server, but the target database is missing or incorrect.\n\nExpected: LibrarySystem\nFound: {currentDb}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // If it fails to connect to SQL Server entirely (e.g., server is down)
                tbox.Text = "SQL Connection Failed!";
                tbox.BackColor = Color.LightCoral;
                
                LockSystem("Could not connect to the SQL Server.\n\nError Details:\n" + ex.Message);
            }
        }
        private void LockSystem(string errorMessage)
        {
            btnManageBooksPage.Enabled = false;
            btnManageMembersPage.Enabled = false;
            btnMainDeskPage.Enabled = false;
            btnAdminPanelPage.Enabled = false;

            MessageBox.Show(errorMessage, "System Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
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