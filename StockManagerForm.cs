using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class StockManagerForm : Form
    {
        // These track whichever book the librarian currently clicked on
        private string selectedIsbn = "";
        private string selectedTitle = "";

        public StockManagerForm()
        {
            InitializeComponent();
        }

        private void StockManagerForm_Load(object sender, EventArgs e)
        {
            LoadStockData();
        }

        private void LoadStockData()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // LEFT JOIN ensures that even books with 0 copies show up in the list!
                string query = @"
                    SELECT 
                        b.ISBN, 
                        b.Title, 
                        b.AuthorName, 
                        bc.CopyNumber, 
                        bc.BookState
                    FROM Books b
                    LEFT JOIN BookCopy bc ON b.ISBN = bc.ISBN
                    ORDER BY b.ISBN";
                
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dgvStock.DataSource = dt;
                    dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            }
        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStock.Rows[e.RowIndex];
                
                selectedIsbn = row.Cells["ISBN"].Value?.ToString();
                selectedTitle = row.Cells["Title"].Value?.ToString();
                
                lblSelectedBook.Text = $"Target Book: {selectedTitle} (ISBN: {selectedIsbn})";
            }
        }

        private void btnAddCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedIsbn))
            {
                MessageBox.Show("Please click on a book in the list first to select which book you are adding stock to.", "Select a Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int newCopyNumber = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();
                    

                    // SMART FEATURE: Auto-generate the next copy number if left blank
                    if (string.IsNullOrWhiteSpace(txtCopyNum.Text))
                    {
                        SqlCommand getNextCmd = new SqlCommand("SELECT ISNULL(MAX(CopyNumber), 0) + 1 FROM BookCopy WHERE ISBN = @isbn", con);
                        getNextCmd.Parameters.AddWithValue("@isbn", selectedIsbn);
                        newCopyNumber = (int)getNextCmd.ExecuteScalar();
                    }
                    else
                    {
                        if (!int.TryParse(txtCopyNum.Text, out newCopyNumber) || newCopyNumber <= 0)
                        {
                            MessageBox.Show("Copy Number must be a valid number greater than 0.");
                            return;
                        }
                    }

                    string query = "INSERT INTO BookCopy (CopyNumber, ISBN, BookState) VALUES (@copyNum, @isbn, 'Available')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@copyNum", newCopyNumber);
                    cmd.Parameters.AddWithValue("@isbn", selectedIsbn);
                    
                    cmd.ExecuteNonQuery();
                }
                
                MessageBox.Show($"Success! Copy #{newCopyNumber} added for '{selectedTitle}'.");
                txtCopyNum.Clear();
                LoadStockData(); // Refresh grid
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("That Copy Number already exists for this book!");
                else MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void btnDeleteCopy_Click(object sender, EventArgs e)
        {
            if (dgvStock.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a specific copy from the grid to delete.");
                return;
            }

            string copyNum = dgvStock.SelectedRows[0].Cells["CopyNumber"].Value.ToString();
            string state = dgvStock.SelectedRows[0].Cells["BookState"].Value.ToString();

            // Handle trying to delete a book that has 0 copies (null copy number)
            if (string.IsNullOrWhiteSpace(copyNum))
            {
                MessageBox.Show("This book currently has 0 copies. There is nothing to delete!");
                return;
            }

            if (state == "Borrowed")
            {
                MessageBox.Show("You cannot delete this copy because it is currently Borrowed out to a member.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Permanently delete Copy #{copyNum} of '{selectedTitle}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                    {
                        string query = "DELETE FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@isbn", selectedIsbn);
                        cmd.Parameters.AddWithValue("@copyNum", copyNum);
                        
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadStockData();
                }
                catch (Exception ex) { MessageBox.Show("Database error: " + ex.Message); }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}