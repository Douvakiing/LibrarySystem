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
                string query = "SELECT StaffID, Name, Email FROM Staff";
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
            
            
            txtStaffEmail.ReadOnly = isSearchMode;
            txtStaffFirst.ReadOnly = isSearchMode; // Used for Name
            txtStaffLast.ReadOnly = isSearchMode;  // Used for Position
            txtStaffPassword.ReadOnly = isSearchMode;
            txtStaffPhone.ReadOnly = isSearchMode;

            btnAddStaff.Enabled = !isSearchMode;
            btnUpdateStaff.Enabled = !isSearchMode;
            btnDeleteStaff.Enabled = !isSearchMode;
            btnSearchStaff.Visible = isSearchMode;
            LoadStaffData();
            ClearStaffInputs();
            
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
                string query = "SELECT StaffID, Name, Email FROM Staff WHERE StaffID = @id";
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
            if (string.IsNullOrWhiteSpace(txtStaffId.Text) || string.IsNullOrWhiteSpace(txtStaffFirst.Text) || string.IsNullOrWhiteSpace(txtStaffLast.Text))
            {
                MessageBox.Show("ID, Name, and Position are required.");
                return;
            }

            // Ensure the ID is a valid number before sending to database
            if (!int.TryParse(txtStaffId.Text, out int staffId))
            {
                MessageBox.Show("Staff ID must be a valid number.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    string query = "INSERT INTO Staff (StaffID, Name, Email) VALUES (@id, @name, @email)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    
                    cmd.Parameters.AddWithValue("@id", staffId); // Now uses your manually typed ID
                    cmd.Parameters.AddWithValue("@name", txtStaffFirst.Text);
                    cmd.Parameters.AddWithValue("@email", txtStaffEmail.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Staff added successfully.");
                LoadStaffData();
                ClearStaffInputs();
            }
            catch (SqlException ex)
            {
                // Error 2627 is a Primary Key violation (ID already exists)
                if (ex.Number == 2627)
                    MessageBox.Show("That Staff ID already exists! Please choose a different ID.");
                else
                    MessageBox.Show("Database error: " + ex.Message);
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
            
            txtPubName.ReadOnly = isSearchMode;
            txtPubEmail.ReadOnly = isSearchMode;
            txtPubPhone.ReadOnly = isSearchMode;
            txtPubAddress.ReadOnly = isSearchMode;

            btnAddPub.Enabled = !isSearchMode;
            btnUpdatePub.Enabled = !isSearchMode;
            btnDeletePub.Enabled = !isSearchMode;
            btnSearchPub.Visible = isSearchMode;
            LoadPublisherData();
            ClearPublisherInputs();
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
            if (string.IsNullOrWhiteSpace(txtPubId.Text) || string.IsNullOrWhiteSpace(txtPubName.Text))
            {
                MessageBox.Show("Publisher ID and Name are required.");
                return;
            }

            if (!int.TryParse(txtPubId.Text, out int pubId))
            {
                MessageBox.Show("Publisher ID must be a valid number.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    // Insert using your manual ID
                    string pubQuery = "INSERT INTO Publisher (PublisherID, PublisherName) VALUES (@id, @name)";
                    SqlCommand cmd = new SqlCommand(pubQuery, con);
                    cmd.Parameters.AddWithValue("@id", pubId);
                    cmd.Parameters.AddWithValue("@name", txtPubName.Text);
                    cmd.ExecuteNonQuery();

                    if (!string.IsNullOrWhiteSpace(txtPubAddress.Text))
                    {
                        string addrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                        SqlCommand addrCmd = new SqlCommand(addrQuery, con);
                        addrCmd.Parameters.AddWithValue("@address", txtPubAddress.Text);
                        addrCmd.Parameters.AddWithValue("@id", pubId);
                        addrCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Publisher added successfully.");
                LoadPublisherData();
                ClearPublisherInputs();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("That Publisher ID already exists! Please choose a different ID.");
                else
                    MessageBox.Show("Database error: " + ex.Message);
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