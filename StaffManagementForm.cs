using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class StaffManagementForm : Form
    {
        public StaffManagementForm()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(1200, 720);
        }

        private void StaffManagementForm_Load(object sender, EventArgs e)
        {
            LoadStaffData();
        }

        private void LoadStaffData()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                string query = "SELECT StaffID, Name, Phone, Password, Email FROM Staff";
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
            
            txtName.ReadOnly = isSearchMode;
            txtPhone.ReadOnly = isSearchMode;
            txtPassword.ReadOnly = isSearchMode;
            txtEmail.ReadOnly = isSearchMode;

            btnAdd.Enabled = !isSearchMode;
            btnUpdate.Enabled = !isSearchMode;
            btnDelete.Enabled = !isSearchMode;
            btnSearch.Visible = isSearchMode;

            LoadStaffData();
            ClearInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                LoadStaffData();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open(); 
                    
                    SqlCommand checkID = new SqlCommand("SELECT StaffID FROM Staff WHERE StaffID=@id", con);
                    checkID.Parameters.AddWithValue("@id", txtId.Text);
                    
                    if (checkID.ExecuteScalar() == null)
                    {
                        MessageBox.Show("No staff exists with this ID");
                        return; // Stop searching if it doesn't exist
                    }

                    string query = "SELECT StaffID, Name, Phone, Password, Email FROM Staff WHERE StaffID = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvStaff.DataSource = dt;
                }
            }
            catch(SqlException ex) { MessageBox.Show("Error: " + ex.Message); }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // ENFORCING SCHEMA: ID, Name, Password, and Email cannot be empty
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Staff ID, Name, Password, and Email are strictly required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int staffId))
            {
                MessageBox.Show("Staff ID must be a valid number.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    string query = "INSERT INTO Staff (StaffID, Name, Phone, Password, Email) VALUES (@id, @name, @phone, @password, @email)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    
                    cmd.Parameters.AddWithValue("@id", staffId);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    
                    // Handle NULL Phone properly
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Staff added successfully.");
                LoadStaffData();
                ClearInputs();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("That Staff ID already exists!");
                else MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // ENFORCING SCHEMA: ID, Name, Password, and Email cannot be empty
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Staff ID, Name, Password, and Email cannot be empty to perform an update.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    string query = @"UPDATE Staff 
                                     SET Name = @name, Phone = @phone, Password = @password, Email = @email 
                                     WHERE StaffID = @id";
                    
                    SqlCommand cmd = new SqlCommand(query, con);
                    
                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    
                    // Handle NULL Phone properly
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Staff updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Staff ID not found. Could not update.");
                    }

                    LoadStaffData(); 
                    ClearInputs();   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    if (ex.Number == 547) MessageBox.Show("Cannot delete this staff member because they manage existing Books.");
                    else MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStaff.Rows[e.RowIndex];
                txtId.Text = row.Cells["StaffID"].Value?.ToString();
                txtName.Text = row.Cells["Name"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtPassword.Text = row.Cells["Password"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
            }
        }

        private void ClearInputs()
        {
            txtId.Clear(); txtName.Clear(); txtPhone.Clear(); txtPassword.Clear(); txtEmail.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Close();
            main.Show();
        }
    }
}