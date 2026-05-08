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
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                string query = @"
                    SELECT 
                        bc.ISBN, 
                        bc.CopyNumber, 
                        b.Title,
                        b.AuthorName
                    FROM BookCopy bc
                    JOIN Books b ON bc.ISBN = b.ISBN
                    WHERE bc.BookState = 'Available'
                    ORDER BY bc.ISBN";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("ISBN");
                dt.Columns.Add("CopyNumber");
                dt.Columns.Add("Title");
                dt.Columns.Add("AuthorName");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["ISBN"] = reader["ISBN"];
                    row["CopyNumber"] = reader["CopyNumber"];
                    row["Title"] = reader["Title"];
                    row["AuthorName"] = reader["AuthorName"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvBookCopies.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void LoadActiveLoans()
        {
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
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

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("CopyNumber");
                dt.Columns.Add("ISBN");
                dt.Columns.Add("Title");
                dt.Columns.Add("LoanDate");
                dt.Columns.Add("DueDate");
                dt.Columns.Add("MemberID");
                dt.Columns.Add("MemberName");

                DataRow row;
                while (reader.Read())
                {
                    row = dt.NewRow();
                    row["CopyNumber"] = reader["CopyNumber"];
                    row["ISBN"] = reader["ISBN"];
                    row["Title"] = reader["Title"];
                    row["LoanDate"] = reader["LoanDate"];
                    row["DueDate"] = reader["DueDate"];
                    row["MemberID"] = reader["MemberID"];
                    row["MemberName"] = reader["MemberName"];
                    dt.Rows.Add(row);
                }
                reader.Close();
                dgvActiveLoans.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            finally { con.Close(); }
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

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();

                // Step 1: Does the Member exist?
                SqlCommand checkMember = new SqlCommand("SELECT COUNT(*) FROM Member WHERE MemberID = @memId", con);
                SqlParameter pMemId = new SqlParameter("@memId", txtIssueMemberId.Text);
                checkMember.Parameters.Add(pMemId);
                
                if ((int)checkMember.ExecuteScalar() == 0)
                {
                    MessageBox.Show("Error: That Member ID does not exist in the system.");
                    return;
                }

                // Step 2: Does the Book (ISBN) exist in the main Books catalog?
                SqlCommand checkISBN = new SqlCommand("SELECT COUNT(*) FROM Books WHERE ISBN = @isbn", con);
                SqlParameter pIsbnCheck = new SqlParameter("@isbn", txtIssueISBN.Text);
                checkISBN.Parameters.Add(pIsbnCheck);
                
                if ((int)checkISBN.ExecuteScalar() == 0)
                {
                    MessageBox.Show("Error: There is no book in the library catalog with this ISBN.");
                    return;
                }

                // Step 3: Does the specific Copy Number exist, and what is its state?
                SqlCommand checkCopy = new SqlCommand("SELECT BookState FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum", con);
                SqlParameter pCopyIsbn = new SqlParameter("@isbn", txtIssueISBN.Text);
                checkCopy.Parameters.Add(pCopyIsbn);
                SqlParameter pCopyNum = new SqlParameter("@copyNum", txtIssueCopyNum.Text);
                checkCopy.Parameters.Add(pCopyNum);
                
                object stateResult = checkCopy.ExecuteScalar();
                
                if (stateResult == null)
                {
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
                SqlParameter pIssueMem = new SqlParameter("@memId", txtIssueMemberId.Text);
                cmd.Parameters.Add(pIssueMem);
                SqlParameter pIssueIsbn = new SqlParameter("@isbn", txtIssueISBN.Text);
                cmd.Parameters.Add(pIssueIsbn);
                SqlParameter pIssueNum = new SqlParameter("@copyNum", txtIssueCopyNum.Text);
                cmd.Parameters.Add(pIssueNum);
                
                cmd.ExecuteNonQuery();

                MessageBox.Show("Book issued successfully! Due in 14 days.");
                txtIssueMemberId.Clear(); txtIssueISBN.Clear(); txtIssueCopyNum.Clear();
                
                LoadAvailableBooks(); 
                LoadActiveLoans(); 
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Error issuing book: " + ex.Message); 
            }
            finally
            {
                con.Close();
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

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();

                SqlCommand checkBook = new SqlCommand("SELECT BookState, DueDate FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum", con);
                SqlParameter pCheckIsbn = new SqlParameter("@isbn", targetIsbn);
                checkBook.Parameters.Add(pCheckIsbn);
                SqlParameter pCheckCopy = new SqlParameter("@copyNum", targetCopyNum);
                checkBook.Parameters.Add(pCheckCopy);
                
                string stateResult = "";
                DateTime dueDate = DateTime.MaxValue;

                SqlDataReader reader = checkBook.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Error: That ISBN / Copy Number combination does not exist.");
                    reader.Close();
                    return;
                }
                stateResult = reader["BookState"].ToString();
                if (reader["DueDate"] != DBNull.Value) dueDate = Convert.ToDateTime(reader["DueDate"]);
                reader.Close(); // Explicitly closing the reader

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
                SqlParameter pRetIsbn = new SqlParameter("@isbn", targetIsbn);
                cmd.Parameters.Add(pRetIsbn);
                SqlParameter pRetCopy = new SqlParameter("@copyNum", targetCopyNum);
                cmd.Parameters.Add(pRetCopy);
                
                cmd.ExecuteNonQuery();

                MessageBox.Show("Book returned successfully!");
                txtReturnISBN.Clear(); txtReturnCopyNum.Clear();
                dgvActiveLoans.ClearSelection();
                
                LoadAvailableBooks(); 
                LoadActiveLoans(); 
            }
            catch (Exception ex) { MessageBox.Show("Error returning book: " + ex.Message); }
            finally { con.Close(); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Close();
            main.Show();
        }
    }
}