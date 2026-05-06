using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class MainDeskForm : Form
    {
        public MainDeskForm()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(1200, 720);
        }

        private void MainDeskForm_Load(object sender, EventArgs e)
        {
            LoadAvailableBooks();
            LoadActiveLoans();
        }

        // ==========================================
        // DATA LOADERS
        // ==========================================
        private void LoadAvailableBooks()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                string query = @"
                    SELECT 
                        bc.ISBN, 
                        bc.CopyNumber, 
                        b.Title,
                        b.AuthorName
                    FROM BookCopy bc
                    JOIN Books b ON bc.ISBN = b.ISBN
                    WHERE bc.BookState = 'Available'
                    ORDER BY b.Title";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dgvBookCopies.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            }
        }

        private void LoadActiveLoans()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                string query = @"
                    SELECT 
                        bc.CopyNumber, 
                        bc.ISBN, 
                        b.Title, 
                        bc.LoanDate, 
                        bc.DueDate, 
                        m.MemberID,
                        m.FirstName + ' ' + m.LastName AS MemberName
                    FROM BookCopy bc
                    JOIN Books b ON bc.ISBN = b.ISBN
                    JOIN Member m ON bc.MemberID = m.MemberID
                    WHERE bc.BookState = 'Borrowed'
                    ORDER BY bc.DueDate ASC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dgvActiveLoans.DataSource = dt;
                }
                catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            }
        }

        // ==========================================
        // SMART UI FEATURES (Auto-Fill & Colors)
        // ==========================================
        private void dgvBookCopies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBookCopies.Rows[e.RowIndex];
                txtIssueISBN.Text = row.Cells["ISBN"].Value?.ToString();
                txtIssueCopyNum.Text = row.Cells["CopyNumber"].Value?.ToString();
            }
        }

        private void dgvActiveLoans_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvActiveLoans.Rows[e.RowIndex];
                txtReturnISBN.Text = row.Cells["ISBN"].Value?.ToString();
                txtReturnCopyNum.Text = row.Cells["CopyNumber"].Value?.ToString();
            }
        }

        private void dgvActiveLoans_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvActiveLoans.Rows)
            {
                if (row.Cells["DueDate"].Value != null && row.Cells["DueDate"].Value != DBNull.Value)
                {
                    DateTime dueDate = Convert.ToDateTime(row.Cells["DueDate"].Value);
                    if (dueDate < DateTime.Today)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
        }

        // ==========================================
        // ACTION BUTTONS
        // ==========================================
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIssueMemberId.Text) || string.IsNullOrWhiteSpace(txtIssueISBN.Text) || string.IsNullOrWhiteSpace(txtIssueCopyNum.Text))
            {
                MessageBox.Show("Please fill out Member ID, ISBN, and Copy Number to issue a book.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    // Step 1: Does the Member exist?
                    SqlCommand checkMember = new SqlCommand("SELECT COUNT(*) FROM Member WHERE MemberID = @memId", con);
                    checkMember.Parameters.AddWithValue("@memId", txtIssueMemberId.Text);
                    if ((int)checkMember.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Error: That Member ID does not exist in the system.");
                        return;
                    }

                    // Step 2: Does the Book (ISBN) exist in the main Books catalog?
                    SqlCommand checkISBN = new SqlCommand("SELECT COUNT(*) FROM Books WHERE ISBN = @isbn", con);
                    checkISBN.Parameters.AddWithValue("@isbn", txtIssueISBN.Text);
                    if ((int)checkISBN.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Error: There is no book in the library catalog with this ISBN.");
                        return;
                    }

                    // Step 3: Does the specific Copy Number exist, and what is its state?
                    SqlCommand checkCopy = new SqlCommand("SELECT BookState FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum", con);
                    checkCopy.Parameters.AddWithValue("@isbn", txtIssueISBN.Text);
                    checkCopy.Parameters.AddWithValue("@copyNum", txtIssueCopyNum.Text);
                    
                    object stateResult = checkCopy.ExecuteScalar();
                    
                    if (stateResult == null)
                    {
                        // Since we already proved the ISBN exists in Step 2, if this is null, it means the copy number is wrong.
                        MessageBox.Show($"Error: The book exists, but Copy Number '{txtIssueCopyNum.Text}' does not exist in the inventory.");
                        return;
                    }

                    // Step 4: Is the copy actually available to borrow?
                    if (stateResult.ToString() != "Available")
                    {
                        MessageBox.Show($"Error: This specific copy cannot be issued because it is currently '{stateResult}'.");
                        return;
                    }

                    // Step 5: All checks passed! Issue the book.
                    string issueQuery = @"
                        UPDATE BookCopy 
                        SET BookState = 'Borrowed', 
                            LoanDate = GETDATE(), 
                            DueDate = DATEADD(day, 14, GETDATE()), 
                            ReturnDate = NULL, 
                            MemberID = @memId 
                        WHERE ISBN = @isbn AND CopyNumber = @copyNum";

                    SqlCommand cmd = new SqlCommand(issueQuery, con);
                    cmd.Parameters.AddWithValue("@memId", txtIssueMemberId.Text);
                    cmd.Parameters.AddWithValue("@isbn", txtIssueISBN.Text);
                    cmd.Parameters.AddWithValue("@copyNum", txtIssueCopyNum.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book issued successfully! Due in 14 days.");
                txtIssueMemberId.Clear(); txtIssueISBN.Clear(); txtIssueCopyNum.Clear();
                
                LoadAvailableBooks(); 
                LoadActiveLoans(); 
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Error issuing book: " + ex.Message); 
            }
        }
        

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string targetIsbn = txtReturnISBN.Text;
            string targetCopyNum = txtReturnCopyNum.Text;

            if (string.IsNullOrWhiteSpace(targetIsbn) || string.IsNullOrWhiteSpace(targetCopyNum))
            {
                MessageBox.Show("Please select a book from the Active Loans radar OR enter the details manually.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    SqlCommand checkBook = new SqlCommand("SELECT BookState, DueDate FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum", con);
                    checkBook.Parameters.AddWithValue("@isbn", targetIsbn);
                    checkBook.Parameters.AddWithValue("@copyNum", targetCopyNum);
                    
                    string stateResult = "";
                    DateTime dueDate = DateTime.MaxValue;

                    using (SqlDataReader reader = checkBook.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Error: That ISBN / Copy Number combination does not exist.");
                            return;
                        }
                        stateResult = reader["BookState"].ToString();
                        if (reader["DueDate"] != DBNull.Value) dueDate = Convert.ToDateTime(reader["DueDate"]);
                    }

                    if (stateResult != "Borrowed")
                    {
                        MessageBox.Show("Error: You cannot return a book that is already marked as Available.");
                        return;
                    }

                    DateTime returnDate = DateTime.Today; 
                    if (returnDate > dueDate)
                    {
                        int daysLate = (int)(returnDate - dueDate).TotalDays;
                        int totalFine = daysLate * 2; 
                        MessageBox.Show($"⚠ OVERDUE NOTICE ⚠\n\nThis book is {daysLate} days late.\nPlease collect a fine of ${totalFine} from the member.", "Overdue Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    string returnQuery = @"
                        UPDATE BookCopy 
                        SET BookState = 'Available', 
                            ReturnDate = GETDATE()
                        WHERE ISBN = @isbn AND CopyNumber = @copyNum";

                    SqlCommand cmd = new SqlCommand(returnQuery, con);
                    cmd.Parameters.AddWithValue("@isbn", targetIsbn);
                    cmd.Parameters.AddWithValue("@copyNum", targetCopyNum);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book returned successfully!");
                txtReturnISBN.Clear(); txtReturnCopyNum.Clear();
                dgvActiveLoans.ClearSelection();
                
                LoadAvailableBooks(); 
                LoadActiveLoans(); 
            }
            catch (Exception ex) { MessageBox.Show("Error returning book: " + ex.Message); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Close();
            main.Show();
        }
    }
}