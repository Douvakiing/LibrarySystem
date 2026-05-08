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
            this.MinimumSize = new System.Drawing.Size(1200, 720);
        }

        private void PublisherManagementForm_Load(object sender, EventArgs e)
        {
            LoadPublisherData();
        }

        private void LoadPublisherData()
        {
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string query = @"SELECT p.PublisherID, p.PublisherName, p.Email, p.Phone, pa.Address 
                                FROM Publisher p 
                                LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("PublisherID");
                dt.Columns.Add("PublisherName");
                dt.Columns.Add("Email");
                dt.Columns.Add("Phone");
                dt.Columns.Add("Address");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["PublisherID"] = reader["PublisherID"];
                    row["PublisherName"] = reader["PublisherName"];
                    row["Email"] = reader["Email"];
                    row["Phone"] = reader["Phone"];
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

        private void btnClear_Click(object sender,EventArgs e)
        {
            ClearInputs();
        }
       private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                LoadPublisherData();
                return;
            }

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open(); 
                SqlCommand checkID = new SqlCommand("SELECT PublisherID FROM Publisher WHERE PublisherID=@id", con);
                SqlParameter pCheckId = new SqlParameter("@id", txtId.Text);
                checkID.Parameters.Add(pCheckId);
                
                if (checkID.ExecuteScalar() == null)
                {
                    MessageBox.Show("No publisher exists with this ID");
                    return; 
                }

                string query = @"SELECT p.PublisherID, p.PublisherName, p.Email, p.Phone, pa.Address 
                                FROM Publisher p 
                                LEFT JOIN PublisherAddress pa ON p.PublisherID = pa.PublisherID 
                                WHERE p.PublisherID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlParameter pId = new SqlParameter("@id", txtId.Text);
                cmd.Parameters.Add(pId);

                // Strictly using Reader
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("PublisherID");
                dt.Columns.Add("PublisherName");
                dt.Columns.Add("Email");
                dt.Columns.Add("Phone");
                dt.Columns.Add("Address");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["PublisherID"] = reader["PublisherID"];
                    row["PublisherName"] = reader["PublisherName"];
                    row["Email"] = reader["Email"];
                    row["Phone"] = reader["Phone"];
                    row["Address"] = reader["Address"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvPublishers.DataSource = dt;
            }
            catch(SqlException ex){ MessageBox.Show("Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Publisher ID, Name, Email, and Phone are strictly required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int pubId) || pubId <= 0)
            {
                MessageBox.Show("Publisher ID must be a valid number greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string pubQuery = "INSERT INTO Publisher (PublisherID, PublisherName, Email, Phone) VALUES (@id, @name, @email, @phone)";
                SqlCommand cmd = new SqlCommand(pubQuery, con);
                
                SqlParameter pId = new SqlParameter("@id", pubId);
                cmd.Parameters.Add(pId);
                SqlParameter pName = new SqlParameter("@name", txtName.Text);
                cmd.Parameters.Add(pName);
                SqlParameter pEmail = new SqlParameter("@email", txtEmail.Text);
                cmd.Parameters.Add(pEmail);
                SqlParameter pPhone = new SqlParameter("@phone", txtPhone.Text);
                cmd.Parameters.Add(pPhone);
                
                cmd.ExecuteNonQuery();

                if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    string addrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                    SqlCommand addrCmd = new SqlCommand(addrQuery, con);
                    SqlParameter pAddr = new SqlParameter("@address", txtAddress.Text);
                    addrCmd.Parameters.Add(pAddr);
                    SqlParameter pAddrId = new SqlParameter("@id", pubId);
                    addrCmd.Parameters.Add(pAddrId);
                    
                    addrCmd.ExecuteNonQuery();
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
            finally { con.Close(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Publisher ID, Name, Email, and Phone cannot be empty to perform an update.");
                return;
            }

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string pubQuery = "UPDATE Publisher SET PublisherName = @name, Email = @email, Phone = @phone WHERE PublisherID = @id";
                SqlCommand cmdPub = new SqlCommand(pubQuery, con);
                
                SqlParameter pId = new SqlParameter("@id", txtId.Text);
                cmdPub.Parameters.Add(pId);
                SqlParameter pName = new SqlParameter("@name", txtName.Text);
                cmdPub.Parameters.Add(pName);
                SqlParameter pEmail = new SqlParameter("@email", txtEmail.Text);
                cmdPub.Parameters.Add(pEmail);
                SqlParameter pPhone = new SqlParameter("@phone", txtPhone.Text);
                cmdPub.Parameters.Add(pPhone);
                
                int rowsAffected = cmdPub.ExecuteNonQuery();

                if (rowsAffected > 0) 
                {
                    string delAddrQuery = "DELETE FROM PublisherAddress WHERE PublisherID = @id";
                    SqlCommand cmdDelAddr = new SqlCommand(delAddrQuery, con);
                    SqlParameter pDelId = new SqlParameter("@id", txtId.Text);
                    cmdDelAddr.Parameters.Add(pDelId);
                    cmdDelAddr.ExecuteNonQuery();

                    if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                    {
                        string insAddrQuery = "INSERT INTO PublisherAddress (Address, PublisherID) VALUES (@address, @id)";
                        SqlCommand cmdInsAddr = new SqlCommand(insAddrQuery, con);
                        SqlParameter pInsId = new SqlParameter("@id", txtId.Text);
                        cmdInsAddr.Parameters.Add(pInsId);
                        SqlParameter pInsAddr = new SqlParameter("@address", txtAddress.Text);
                        cmdInsAddr.Parameters.Add(pInsAddr);
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
            catch (Exception ex) { MessageBox.Show("Database error: " + ex.Message); }
            finally { con.Close(); }
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
                SqlConnection con = new SqlConnection(Program.ConnectionString);
                try
                {
                    con.Open();
                    string deleteAddrQuery = "DELETE FROM PublisherAddress WHERE PublisherID = @id";
                    SqlCommand cmdAddr = new SqlCommand(deleteAddrQuery, con);
                    SqlParameter pAddrId = new SqlParameter("@id", pubId);
                    cmdAddr.Parameters.Add(pAddrId);
                    cmdAddr.ExecuteNonQuery();

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