using Microsoft.VisualBasic;
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

namespace LibrarySystem
{
    public partial class MemberDirectoryForm : Form
    {
        public MemberDirectoryForm()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(1200, 720);
        }
        private void MemberDirectoryForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Querying your real Books table
                string query = "SELECT * FROM Member";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();

                try
                {
                    adapter.Fill(dataTable);
                    dgvMembers.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            AddMemberForm popup = new AddMemberForm();
            popup.ShowDialog(); 
            RefreshGrid();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            // 1. Prompt for ISBN
            string memberID = Interaction.InputBox("Enter the Member ID of the member to delete:", "Member ID", "");

            if (!string.IsNullOrWhiteSpace(memberID))
            {
                // 2. Confirm
                var confirm = MessageBox.Show($"Delete Member {memberID}?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                    {
                        string query = "DELETE FROM Member WHERE MemberID = @memberID";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@memberID", memberID);

                        try
                        {
                            con.Open();
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0) MessageBox.Show("Deleted.");
                            else MessageBox.Show("memberID not found.");

                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 547)
                            {
                                MessageBox.Show("This member currently has a book borrowed! " +
                                                "You must wait until the member returns the book and remove them from the Circulation Desk before deleting the member.",
                                                "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                // Something else went wrong (server down, etc.)
                                MessageBox.Show("A database error occurred: " + ex.Message, "Error");
                            }
                        }
                        finally
                        {
                            RefreshGrid();
                            con.Close();
                        }
                    }
                }
            }
        }

        private void btnUpdateMember_Click(object sender, EventArgs e)
        {
            UpdateMemberForm popup = new UpdateMemberForm();
            popup.ShowDialog(); // ShowDialog freezes the main form until the popup is closed
            RefreshGrid();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
