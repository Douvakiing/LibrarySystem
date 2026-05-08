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
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT StaffID, Name, Email FROM Staff", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("StaffID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Email");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["StaffID"] = reader["StaffID"];
                    row["Name"] = reader["Name"];
                    row["Email"] = reader["Email"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvStaff.DataSource = dt;
                dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            finally { con.Close(); }
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

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string query = "SELECT StaffID, Name, Email FROM Staff WHERE StaffID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                
                SqlParameter pId = new SqlParameter("@id", txtStaffId.Text);
                cmd.Parameters.Add(pId);
                
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("StaffID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Email");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["StaffID"] = reader["StaffID"];
                    row["Name"] = reader["Name"];
                    row["Email"] = reader["Email"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvStaff.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Database error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStaffId.Text) || string.IsNullOrWhiteSpace(txtStaffFirst.Text) || string.IsNullOrWhiteSpace(txtStaffLast.Text))
            {
                MessageBox.Show("ID, Name, and Position are required.");
                return;
            }

            if (!int.TryParse(txtStaffId.Text, out int staffId))
            {
                MessageBox.Show("Staff ID must be a valid number.");
                return;
            }

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string query = "INSERT INTO Staff (StaffID, Name, Email) VALUES (@id, @name, @email)";
                SqlCommand cmd = new SqlCommand(query, con);
                
                SqlParameter pId = new SqlParameter("@id", staffId);
                cmd.Parameters.Add(pId);
                
                SqlParameter pName = new SqlParameter("@name", txtStaffFirst.Text);
                cmd.Parameters.Add(pName);
                
                SqlParameter pEmail = new SqlParameter("@email", txtStaffEmail.Text);
                cmd.Parameters.Add(pEmail);

                cmd.ExecuteNonQuery();
                
                MessageBox.Show("Staff added successfully.");
                LoadStaffData();
                ClearStaffInputs();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("That Staff ID already exists! Please choose a different ID.");
                else MessageBox.Show("Database error: " + ex.Message);
            }
            finally { con.Close(); }
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
                SqlConnection con = new SqlConnection(Program.ConnectionString);
                try
                {
                    con.Open();
                    string query = "DELETE FROM Staff WHERE StaffID = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    
                    SqlParameter pId = new SqlParameter("@id", staffId);
                    cmd.Parameters.Add(pId);
                    
                    cmd.ExecuteNonQuery();
                    LoadStaffData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) MessageBox.Show("Cannot delete this staff member because they are assigned to existing Books.");
                    else MessageBox.Show("Database error: " + ex.Message);
                }
                finally { con.Close(); }
            }
        }

        private void ClearStaffInputs()
        {
            txtStaffId.Clear(); txtStaffFirst.Clear(); txtStaffLast.Clear();
            txtStaffEmail.Clear(); txtStaffPhone.Clear(); txtStaffPassword.Clear();
        }

         private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select a record in the grid to update (Implementation depends on your specific update flow).");
        }
        // ==========================================
        // TAB 2: PUBLISHERS
        // ==========================================
        private void LoadPublisherData()
        {
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string query = @"SELECT p.PublisherID, p.PublisherName, pa.Address 
                                FROM Publisher p 
                                LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("PublisherID");
                dt.Columns.Add("PublisherName");
                dt.Columns.Add("Address");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["PublisherID"] = reader["PublisherID"];
                    row["PublisherName"] = reader["PublisherName"];
                    row["Address"] = reader["Address"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvPublishers.DataSource = dt;
                dgvPublishers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            finally { con.Close(); }
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

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string query = @"SELECT p.PublisherID, p.PublisherName, pa.Address 
                                FROM Publisher p 
                                LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID 
                                WHERE p.PublisherID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                
                SqlParameter pId = new SqlParameter("@id", txtPubId.Text);
                cmd.Parameters.Add(pId);
                
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("PublisherID");
                dt.Columns.Add("PublisherName");
                dt.Columns.Add("Address");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["PublisherID"] = reader["PublisherID"];
                    row["PublisherName"] = reader["PublisherName"];
                    row["Address"] = reader["Address"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvPublishers.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Database error: " + ex.Message); }
            finally { con.Close(); }
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

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();

                string pubQuery = "INSERT INTO Publisher (PublisherID, PublisherName) VALUES (@id, @name)";
                SqlCommand cmd = new SqlCommand(pubQuery, con);
                
                SqlParameter pId = new SqlParameter("@id", pubId);
                cmd.Parameters.Add(pId);
                
                SqlParameter pName = new SqlParameter("@name", txtPubName.Text);
                cmd.Parameters.Add(pName);
                
                cmd.ExecuteNonQuery();

                if (!string.IsNullOrWhiteSpace(txtPubAddress.Text))
                {
                    string addrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                    SqlCommand addrCmd = new SqlCommand(addrQuery, con);
                    
                    SqlParameter pAddr = new SqlParameter("@address", txtPubAddress.Text);
                    addrCmd.Parameters.Add(pAddr);
                    
                    SqlParameter pAddrId = new SqlParameter("@id", pubId);
                    addrCmd.Parameters.Add(pAddrId);
                    
                    addrCmd.ExecuteNonQuery();
                }
                
                MessageBox.Show("Publisher added successfully.");
                LoadPublisherData();
                ClearPublisherInputs();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("That Publisher ID already exists! Please choose a different ID.");
                else MessageBox.Show("Database error: " + ex.Message);
            }
            finally { con.Close(); }
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
                SqlConnection con = new SqlConnection(Program.ConnectionString);
                try
                {
                    con.Open();

                    // Delete address FIRST due to constraints
                    string deleteAddrQuery = "DELETE FROM PublisherAddress WHERE PublisherID = @id";
                    SqlCommand cmdAddr = new SqlCommand(deleteAddrQuery, con);
                    
                    SqlParameter pAddrId = new SqlParameter("@id", pubId);
                    cmdAddr.Parameters.Add(pAddrId);
                    
                    cmdAddr.ExecuteNonQuery();

                    // Delete Publisher
                    string deletePubQuery = "DELETE FROM Publisher WHERE PublisherID = @id";
                    SqlCommand cmdPub = new SqlCommand(deletePubQuery, con);
                    
                    SqlParameter pPubId = new SqlParameter("@id", pubId);
                    cmdPub.Parameters.Add(pPubId);
                    
                    cmdPub.ExecuteNonQuery();
                    
                    LoadPublisherData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) MessageBox.Show("Cannot delete this publisher because they have existing Books attached to them.");
                    else MessageBox.Show("Database error: " + ex.Message);
                }
                finally { con.Close(); }
            }
}
        private void btnUpdatePub_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select a record in the grid to update.");
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