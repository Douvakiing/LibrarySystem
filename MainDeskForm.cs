using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class MainDeskForm : Form
    {
        public MainDeskForm()
        {
            InitializeComponent();
        }

        private void MainDeskForm_Load(object sender, EventArgs e)
        {
            LoadActiveLoans();
        }

        // ==========================================
        // ZONE C: THE LIVE RADAR (ACTIVE LOANS)
        // ==========================================
        private void LoadActiveLoans()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Joins BookCopy, Books, and Member to show a clean, human-readable radar of missing books
                string query = @"
                    SELECT 
                        bc.CopyNumber, 
                        bc.ISBN, 
                        b.Title, 
                        bc.LoanDate, 
                        bc.DueDate, 
                        m.FirstName + ' ' + m.LastName AS MemberName,
                        m.Phone
                    FROM BookCopy bc
                    JOIN Books b ON bc.ISBN = b.ISBN
                    JOIN Member m ON bc.MemberID = m.MemberID
                    WHERE bc.BookState = 'Borrowed'
                    ORDER BY bc.DueDate ASC"; // Books due soonest show at the top!

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dgvActiveLoans.DataSource = dt;
                    dgvActiveLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            }
        }

        // ==========================================
        // ZONE A: CHECK-OUT (ISSUE BOOK)
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

                    // Rule 1: Does the Member exist?
                    SqlCommand checkMember = new SqlCommand("SELECT COUNT(*) FROM Member WHERE MemberID = @memId", con);
                    checkMember.Parameters.AddWithValue("@memId", txtIssueMemberId.Text);
                    if ((int)checkMember.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Error: That Member ID does not exist in the system.");
                        return;
                    }

                    // Rule 2: Does this specific copy exist AND is it Available?
                    SqlCommand checkBook = new SqlCommand("SELECT BookState FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum", con);
                    checkBook.Parameters.AddWithValue("@isbn", txtIssueISBN.Text);
                    checkBook.Parameters.AddWithValue("@copyNum", txtIssueCopyNum.Text);
                    
                    object stateResult = checkBook.ExecuteScalar();
                    if (stateResult == null)
                    {
                        MessageBox.Show("Error: That ISBN / Copy Number combination does not exist in the inventory.");
                        return;
                    }
                    if (stateResult.ToString() != "Available")
                    {
                        MessageBox.Show($"Error: This book cannot be issued because it is currently '{stateResult}'.");
                        return;
                    }

                    // Rule 3: All clear! Check it out to the member.
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
                LoadActiveLoans(); // Update radar
            }
            catch (Exception ex) { MessageBox.Show("Error issuing book: " + ex.Message); }
        }

        // ==========================================
        // ZONE B: DROP BOX (RETURN BOOK)
        // ==========================================
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReturnISBN.Text) || string.IsNullOrWhiteSpace(txtReturnCopyNum.Text))
            {
                MessageBox.Show("Please enter the ISBN and Copy Number to process a return.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                {
                    con.Open();

                    // Rule 1: Ensure the book is actually checked out
                    SqlCommand checkBook = new SqlCommand("SELECT BookState FROM BookCopy WHERE ISBN = @isbn AND CopyNumber = @copyNum", con);
                    checkBook.Parameters.AddWithValue("@isbn", txtReturnISBN.Text);
                    checkBook.Parameters.AddWithValue("@copyNum", txtReturnCopyNum.Text);
                    
                    object stateResult = checkBook.ExecuteScalar();
                    if (stateResult == null)
                    {
                        MessageBox.Show("Error: That ISBN / Copy Number combination does not exist.");
                        return;
                    }
                    if (stateResult.ToString() != "Borrowed")
                    {
                        MessageBox.Show("Error: You cannot return a book that is already marked as Available.");
                        return;
                    }

                    // Rule 2: Process the return. (We update State, set ReturnDate to today, but keep the MemberID attached for historical tracking!)
                    string returnQuery = @"
                        UPDATE BookCopy 
                        SET BookState = 'Available', 
                            ReturnDate = GETDATE()
                        WHERE ISBN = @isbn AND CopyNumber = @copyNum";

                    SqlCommand cmd = new SqlCommand(returnQuery, con);
                    cmd.Parameters.AddWithValue("@isbn", txtReturnISBN.Text);
                    cmd.Parameters.AddWithValue("@copyNum", txtReturnCopyNum.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Book returned successfully! It is now back in circulation.");
                txtReturnISBN.Clear(); txtReturnCopyNum.Clear();
                LoadActiveLoans(); // Radar will automatically clear this row!
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