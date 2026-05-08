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
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                
                // 1. Point to the stored procedure instead of raw SQL
                SqlCommand cmd = new SqlCommand("sp_GetCurrentDatabase", con);
                
                // 2. Tell ADO.NET that this is a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                
                // 3. Execute just like before
                string currentDb = cmd.ExecuteScalar()?.ToString();
                
                if (currentDb != null && currentDb.Equals("LibrarySystem", StringComparison.OrdinalIgnoreCase))
                {
                    tbox.Text = "Connected to LibrarySystem DB!";
                    tbox.BackColor = Color.LightGreen;
                }
                else
                {
                    tbox.Text = $"Wrong DB: {currentDb}";
                    tbox.BackColor = Color.LightCoral;
                    LockSystem($"Connected to SQL Server, but the target database is missing or incorrect.\n\nExpected: LibrarySystem\nFound: {currentDb}");
                }
            }
            catch (SqlException ex)
            {
                tbox.Text = "SQL Connection Failed!";
                tbox.BackColor = Color.LightCoral;
                LockSystem("Could not connect to the SQL Server.\n\nError Details:\n" + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        
        private void LockSystem(string errorMessage)
        {
            btnManageBooksPage.Enabled = false;
            btnManageMembersPage.Enabled = false;
            btnMainDeskPage.Enabled = false;
            btnManageStaff.Enabled = false;
            btnManagePublishers.Enabled = false;

            MessageBox.Show(errorMessage, "System Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnManageBooksPage_Click_1(object sender, EventArgs e)
        {
            BookManagerForm bookForm = new BookManagerForm();
            bookForm.Show();
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

        // --- NEW BUTTONS TO REPLACE ADMIN PANEL ---
        private void btnManageStaff_Click(object sender, EventArgs e)
        {
            // Assuming you named your split form 'StaffManagementForm'
            StaffManagementForm staffForm = new StaffManagementForm();
            staffForm.Show();
            this.Hide();
        }

        private void btnManagePublishers_Click(object sender, EventArgs e)
        {
            // Assuming you named your split form 'PublisherManagementForm'
            PublisherManagementForm pubForm = new PublisherManagementForm();
            pubForm.Show();
            this.Hide();
        }
    }
}