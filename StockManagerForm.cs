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
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
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
                
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("ISBN");
                dt.Columns.Add("Title");
                dt.Columns.Add("AuthorName");
                dt.Columns.Add("CopyNumber");
                dt.Columns.Add("BookState");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["ISBN"] = reader["ISBN"];
                    row["Title"] = reader["Title"];
                    row["AuthorName"] = reader["AuthorName"];
                    row["CopyNumber"] = reader["CopyNumber"];
                    row["BookState"] = reader["BookState"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvStock.DataSource = dt;
                dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            finally { con.Close(); }
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
                MessageBox.Show("Please click on a book in the list first.");
                return;
            }
            
            int newCopyNumber = 0;
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                if (string.IsNullOrWhiteSpace(txtCopyNum.Text))
                {
                    SqlCommand getNextCmd = new SqlCommand("SELECT ISNULL(MAX(CopyNumber), 0) + 1 FROM BookCopy WHERE ISBN = @isbn", con);
                    SqlParameter pIsbn = new SqlParameter("@isbn", selectedIsbn);
                    getNextCmd.Parameters.Add(pIsbn);
                    newCopyNumber = (int)getNextCmd.ExecuteScalar();
                }
                else
                {
                    if (!int.TryParse(txtCopyNum.Text, out newCopyNumber) || newCopyNumber <= 0)
                    {
                        MessageBox.Show("Copy Number must be a valid number greater than 0.");
                        con.Close();
                        return;
                    }
                }

                string query = "INSERT INTO BookCopy (CopyNumber, ISBN, BookState) VALUES (@copyNum, @isbn, 'Available')";
                SqlCommand cmd = new SqlCommand(query, con);
                
                SqlParameter pCopyNum = new SqlParameter("@copyNum", newCopyNumber);
                cmd.Parameters.Add(pCopyNum);
                
                SqlParameter pIsbnInsert = new SqlParameter("@isbn", selectedIsbn);
                cmd.Parameters.Add(pIsbnInsert);
                
                cmd.ExecuteNonQuery();
                
                MessageBox.Show($"Success! Copy #{newCopyNumber} added for '{selectedTitle}'.");
                txtCopyNum.Clear();
                LoadStockData(); 
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) MessageBox.Show("That Copy Number already exists for this book!");
                else MessageBox.Show("Database error: " + ex.Message);
            }
            finally { con.Close(); }
        }

        private void btnDeleteCopy_Click(object sender, EventArgs e)
        {
            if (dgvStock.SelectedRows.Count == 0) return;

            string copyNum = dgvStock.SelectedRows[0].Cells["CopyNumber"].Value.ToString();
            string state = dgvStock.SelectedRows[0].Cells["BookState"].Value.ToString();

            if (string.IsNullOrWhiteSpace(copyNum)) return;

            if (state == "Borrowed")
            {
                MessageBox.Show("You cannot delete this copy because it is currently Borrowed.");
                return;
            }

            var confirm = MessageBox.Show($"Permanently delete Copy #{copyNum} of '{selectedTitle}'?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(Program.ConnectionString);
                try
                {
                    con.Open();
                    string query = "DELETE FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum";
                    SqlCommand cmd = new SqlCommand(query, con);
                    
                    SqlParameter pIsbn = new SqlParameter("@isbn", selectedIsbn);
                    cmd.Parameters.Add(pIsbn);
                    SqlParameter pCopyNum = new SqlParameter("@copyNum", copyNum);
                    cmd.Parameters.Add(pCopyNum);
                    
                    cmd.ExecuteNonQuery();
                    LoadStockData();
                }
                catch (Exception ex) { MessageBox.Show("Database error: " + ex.Message); }
                finally { con.Close(); }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}