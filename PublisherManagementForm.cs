using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class PublisherManagementForm : Form
    {
        public PublisherManagementForm()
        {
            InitializeComponent();
        }

        private void PublisherManagementForm_Load(object sender, EventArgs e)
        {
            LoadPublisherData();
        }

        private void LoadPublisherData()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                string query = @"SELECT p.PublisherID, p.PublisherName, p.Email, p.Phone, pa.Address 
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

        private void chkSearchMode_CheckedChanged(object sender, EventArgs e)
        {
            bool isSearchMode = chkSearchMode.Checked;
            
            txtName.ReadOnly = isSearchMode;
            txtEmail.ReadOnly = isSearchMode;
            txtPhone.ReadOnly = isSearchMode;
            txtAddress.ReadOnly = isSearchMode;

            btnAdd.Enabled = !isSearchMode;
            btnUpdate.Enabled = !isSearchMode;
            btnDelete.Enabled = !isSearchMode;
            btnSearch.Visible = isSearchMode;

            LoadPublisherData();
            ClearInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                LoadPublisherData();
                return;
            }

            try{
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open(); 
                        
                        SqlCommand checkID = new SqlCommand("SELECT PublisherID FROM Publisher WHERE PublisherID=@id", con);
                        checkID.Parameters.AddWithValue("@id", txtId.Text);
                        
                        if (checkID.ExecuteScalar() == null)
                        {
                            MessageBox.Show("No staff exists with this ID");
                            return; // Stop searching if it doesn't exist
                        }
                    string query = @"SELECT p.PublisherID, p.PublisherName, p.Email, p.Phone, pa.Address 
                                    FROM Publisher p 
                                    LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID 
                                    WHERE p.PublisherID = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPublishers.DataSource = dt;
                }
            }
            catch(SqlException ex){ MessageBox.Show("Error: " + ex.Message); }
        }
        private void btnClear_Click(object sender,EventArgs e)
        {
            ClearInputs();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // ENFORCING SCHEMA: ID, Name, Email, and Phone cannot be empty
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Publisher ID, Name, Email, and Phone are strictly required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ENFORCING SCHEMA: PublisherID must be > 0
            if (!int.TryParse(txtId.Text, out int pubId) || pubId <= 0)
            {
                MessageBox.Show("Publisher ID must be a valid number greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();
                    string pubQuery = "INSERT INTO Publisher (PublisherID, PublisherName, Email, Phone) VALUES (@id, @name, @email, @phone)";
                    SqlCommand cmd = new SqlCommand(pubQuery, con);
                    cmd.Parameters.AddWithValue("@id", pubId);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.ExecuteNonQuery();

                    if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                    {
                        string addrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                        SqlCommand addrCmd = new SqlCommand(addrQuery, con);
                        addrCmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        addrCmd.Parameters.AddWithValue("@id", pubId);
                        addrCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Publisher added successfully.");
                LoadPublisherData();
                ClearInputs();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("That Publisher ID already exists!");
                else MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // ENFORCING SCHEMA: Cannot update if the required fields are cleared out
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Publisher ID, Name, Email, and Phone cannot be empty to perform an update.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    string pubQuery = @"UPDATE Publisher 
                                        SET PublisherName = @name, Email = @email, Phone = @phone 
                                        WHERE PublisherID = @id";
                    
                    SqlCommand cmdPub = new SqlCommand(pubQuery, con);
                    cmdPub.Parameters.AddWithValue("@id", txtId.Text);
                    cmdPub.Parameters.AddWithValue("@name", txtName.Text);
                    cmdPub.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmdPub.Parameters.AddWithValue("@phone", txtPhone.Text);
                    
                    int rowsAffected = cmdPub.ExecuteNonQuery();

                    if (rowsAffected > 0) 
                    {
                        string delAddrQuery = "DELETE FROM PublisherAddress WHERE PublisherID = @id";
                        SqlCommand cmdDelAddr = new SqlCommand(delAddrQuery, con);
                        cmdDelAddr.Parameters.AddWithValue("@id", txtId.Text);
                        cmdDelAddr.ExecuteNonQuery();

                        if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                        {
                            string insAddrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                            SqlCommand cmdInsAddr = new SqlCommand(insAddrQuery, con);
                            cmdInsAddr.Parameters.AddWithValue("@id", txtId.Text);
                            cmdInsAddr.Parameters.AddWithValue("@address", txtAddress.Text);
                            cmdInsAddr.ExecuteNonQuery();
                        }

                        MessageBox.Show("Publisher updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Publisher ID not found. Could not update.");
                    }

                    LoadPublisherData(); 
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
                        string deleteAddrQuery = "DELETE FROM PublisherAddress WHERE PublisherID = @id";
                        SqlCommand cmdAddr = new SqlCommand(deleteAddrQuery, con);
                        cmdAddr.Parameters.AddWithValue("@id", pubId);
                        cmdAddr.ExecuteNonQuery();

                        string deletePubQuery = "DELETE FROM Publisher WHERE PublisherID = @id";
                        SqlCommand cmdPub = new SqlCommand(deletePubQuery, con);
                        cmdPub.Parameters.AddWithValue("@id", pubId);
                        cmdPub.ExecuteNonQuery();
                    }
                    LoadPublisherData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) MessageBox.Show("Cannot delete this publisher because they have existing Books attached to them.");
                    else MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void dgvPublishers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPublishers.Rows[e.RowIndex];
                txtId.Text = row.Cells["PublisherID"].Value?.ToString();
                txtName.Text = row.Cells["PublisherName"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
            }
        }

        private void ClearInputs()
        {
            txtId.Clear(); txtName.Clear(); txtEmail.Clear(); txtPhone.Clear(); txtAddress.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Close();
            main.Show();
        }
    }
}