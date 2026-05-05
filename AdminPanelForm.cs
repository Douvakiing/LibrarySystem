using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class AdminPanelForm : Form
    {
        public AdminPanelForm()
        {
            InitializeComponent();
        }

        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            LoadStaffData();
            LoadPublisherData();
        }

        // ==========================================
        // TAB 1: STAFF MANAGEMENT
        // ==========================================
        private void LoadStaffData()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Updated to match your exact schema columns
                string query = "SELECT StaffID, Name, Position, Email FROM Staff";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dgvStaff.DataSource = dt;
                    dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            }
        }

        private void chkSearchMode_CheckedChanged(object sender, EventArgs e)
        {
            bool isSearchMode = chkSearchMode.Checked;
            txtStaffId.ReadOnly = !isSearchMode; 
            
            txtStaffEmail.ReadOnly = isSearchMode;
            txtStaffFirst.ReadOnly = isSearchMode; // Used for Name
            txtStaffLast.ReadOnly = isSearchMode;  // Used for Position
            txtStaffPassword.ReadOnly = isSearchMode;
            txtStaffPhone.ReadOnly = isSearchMode;

            btnAddStaff.Enabled = !isSearchMode;
            btnUpdateStaff.Enabled = !isSearchMode;
            btnDeleteStaff.Enabled = !isSearchMode;
            btnSearchStaff.Visible = isSearchMode;
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStaffId.Text))
            {
                LoadStaffData(); 
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                string query = "SELECT StaffID, Name, Position, Email FROM Staff WHERE StaffID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", txtStaffId.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvStaff.DataSource = dt;
            }
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            // txtStaffFirst acts as Name, txtStaffLast acts as Position
            if (string.IsNullOrWhiteSpace(txtStaffFirst.Text) || string.IsNullOrWhiteSpace(txtStaffLast.Text))
            {
                MessageBox.Show("Name and Position are required.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    // 1. Calculate the next available StaffID since it's not an IDENTITY column
                    int nextId = 1;
                    SqlCommand idCmd = new SqlCommand("SELECT ISNULL(MAX(StaffID), 0) + 1 FROM Staff", con);
                    nextId = Convert.ToInt32(idCmd.ExecuteScalar());

                    // 2. Insert into the database
                    string query = "INSERT INTO Staff (StaffID, Name, Position, Email) VALUES (@id, @name, @position, @email)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", nextId);
                    cmd.Parameters.AddWithValue("@name", txtStaffFirst.Text);     // Mapped to First Name box
                    cmd.Parameters.AddWithValue("@position", txtStaffLast.Text);  // Mapped to Last Name box
                    cmd.Parameters.AddWithValue("@email", txtStaffEmail.Text);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Staff added successfully.");
                LoadStaffData();
                ClearStaffInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding staff: " + ex.Message);
            }
        }

        private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select a record in the grid to update (Implementation depends on your specific update flow).");
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an entire row in the grid to delete.");
                return;
            }

            string staffId = dgvStaff.SelectedRows[0].Cells["StaffID"].Value.ToString();
            var confirm = MessageBox.Show($"Delete Staff ID {staffId}?", "Confirm", MessageBoxButtons.YesNo);
            
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                    {
                        string query = "DELETE FROM Staff WHERE StaffID = @id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", staffId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadStaffData();
                }
                catch (SqlException ex)
                {
                    // Error 547 is a Foreign Key violation (trying to delete staff who manages books)
                    if (ex.Number == 547) 
                        MessageBox.Show("Cannot delete this staff member because they are assigned to existing Books.");
                    else 
                        MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void ClearStaffInputs()
        {
            txtStaffId.Clear(); txtStaffFirst.Clear(); txtStaffLast.Clear();
            txtStaffEmail.Clear(); txtStaffPhone.Clear(); txtStaffPassword.Clear();
        }

        // ==========================================
        // TAB 2: PUBLISHERS
        // ==========================================
        private void LoadPublisherData()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Join Publisher with PublisherAddress so we can see everything in one grid
                string query = @"SELECT p.PublisherID, p.PublisherName, pa.Address 
                                 FROM Publisher p 
                                 LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dgvPublishers.DataSource = dt;
                    dgvPublishers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            }
        }

        private void chkSearchPub_CheckedChanged(object sender, EventArgs e)
        {
            bool isSearchMode = chkSearchPub.Checked;
            txtPubId.ReadOnly = !isSearchMode;
            
            txtPubName.ReadOnly = isSearchMode;
            txtPubEmail.ReadOnly = isSearchMode;
            txtPubPhone.ReadOnly = isSearchMode;
            txtPubAddress.ReadOnly = isSearchMode;

            btnAddPub.Enabled = !isSearchMode;
            btnUpdatePub.Enabled = !isSearchMode;
            btnDeletePub.Enabled = !isSearchMode;
            btnSearchPub.Visible = isSearchMode;
        }

        private void btnSearchPub_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPubId.Text))
            {
                LoadPublisherData();
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                string query = @"SELECT p.PublisherID, p.PublisherName, pa.Address 
                                 FROM Publisher p 
                                 LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID 
                                 WHERE p.PublisherID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", txtPubId.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPublishers.DataSource = dt;
            }
        }

        private void btnAddPub_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPubName.Text))
            {
                MessageBox.Show("Publisher Name is required.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    // 1. Calculate next ID
                    int nextId = 1;
                    SqlCommand idCmd = new SqlCommand("SELECT ISNULL(MAX(PublisherID), 0) + 1 FROM Publisher", con);
                    nextId = Convert.ToInt32(idCmd.ExecuteScalar());

                    // 2. Insert the Publisher
                    string pubQuery = "INSERT INTO Publisher (PublisherID, PublisherName) VALUES (@id, @name)";
                    SqlCommand cmd = new SqlCommand(pubQuery, con);
                    cmd.Parameters.AddWithValue("@id", nextId);
                    cmd.Parameters.AddWithValue("@name", txtPubName.Text);
                    cmd.ExecuteNonQuery();

                    // 3. Insert the Address into the child table if provided
                    if (!string.IsNullOrWhiteSpace(txtPubAddress.Text))
                    {
                        string addrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                        SqlCommand addrCmd = new SqlCommand(addrQuery, con);
                        addrCmd.Parameters.AddWithValue("@address", txtPubAddress.Text);
                        addrCmd.Parameters.AddWithValue("@id", nextId);
                        addrCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Publisher added successfully.");
                LoadPublisherData();
                ClearPublisherInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding publisher: " + ex.Message);
            }
        }

        private void btnUpdatePub_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select a record in the grid to update.");
        }

        private void btnDeletePub_Click(object sender, EventArgs e)
        {
            if (dgvPublishers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an entire row in the grid to delete.");
                return;
            }

            string pubId = dgvPublishers.SelectedRows[0].Cells["PublisherID"].Value.ToString();
            var confirm = MessageBox.Show($"Delete Publisher ID {pubId}?", "Confirm", MessageBoxButtons.YesNo);
            
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                    {
                        con.Open();

                        // IMPORTANT: Because PublisherAddress relies on PublisherID, we MUST delete the address FIRST
                        string deleteAddrQuery = "DELETE FROM PublisherAddress WHERE PublisherID = @id";
                        SqlCommand cmdAddr = new SqlCommand(deleteAddrQuery, con);
                        cmdAddr.Parameters.AddWithValue("@id", pubId);
                        cmdAddr.ExecuteNonQuery();

                        // Now it is safe to delete the Publisher
                        string deletePubQuery = "DELETE FROM Publisher WHERE PublisherID = @id";
                        SqlCommand cmdPub = new SqlCommand(deletePubQuery, con);
                        cmdPub.Parameters.AddWithValue("@id", pubId);
                        cmdPub.ExecuteNonQuery();
                    }
                    LoadPublisherData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) 
                        MessageBox.Show("Cannot delete this publisher because they have existing Books attached to them.");
                    else 
                        MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void ClearPublisherInputs()
        {
            txtPubId.Clear(); txtPubName.Clear(); txtPubEmail.Clear();
            txtPubPhone.Clear(); txtPubAddress.Clear();
        }

        // ==========================================
        // GLOBAL NAVIGATION
        // ==========================================
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Close(); 
            main.Show();
        }
    }
}