using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class BookManagerForm : Form
    {
        public BookManagerForm()
        {
            InitializeComponent();
        }

        private void BookManagerForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Querying your real Books table
                string query = "SELECT * FROM Books";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();

                try
                {
                    adapter.Fill(dataTable);
                    // Check your GridView name in Properties; it might be dataGridView1
                    dgvBooks.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddBookForm popup = new AddBookForm();
            popup.ShowDialog(); // ShowDialog freezes the main form until the popup is closed
            RefreshGrid();      // Refresh the grid once the user is done adding
        }

        // Helper method to clear the text boxes after a successful add
        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           UpdateBookForm popup = new UpdateBookForm();
           popup.ShowDialog(); // ShowDialog freezes the main form until the popup is closed
           RefreshGrid();      // Refresh the grid once the user is done updating
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string isbn = Interaction.InputBox("Enter the ISBN of the book to delete:", "Delete Book", "");

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete book {isbn} and ALL of its copies?",
                                              "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                    {
                        con.Open();
                        // Start a transaction so all steps happen together or not at all
                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            // STEP 1: Check if any copy is currently 'Checked Out'
                            string checkQuery = "SELECT COUNT(*) FROM BookCopy WHERE ISBN = @isbn AND BookState = 'Borrowed'";
                            SqlCommand checkCmd = new SqlCommand(checkQuery, con, transaction);
                            checkCmd.Parameters.AddWithValue("@isbn", isbn);

                            int borrowedCount = (int)checkCmd.ExecuteScalar();

                            if (borrowedCount > 0)
                            {
                                MessageBox.Show($"Action Denied: {borrowedCount} copy/copies of this book are currently borrowed!",
                                                "Book is Borrowed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                transaction.Rollback(); // Cancel everything
                                return;
                            }

                            // STEP 2: Delete all copies from BookCopy (since we know none are borrowed)
                            string delCopiesQuery = "DELETE FROM BookCopy WHERE ISBN = @isbn";
                            SqlCommand delCopiesCmd = new SqlCommand(delCopiesQuery, con, transaction);
                            delCopiesCmd.Parameters.AddWithValue("@isbn", isbn);
                            delCopiesCmd.ExecuteNonQuery();

                            // STEP 3: Delete the main book record
                            string delBookQuery = "DELETE FROM Books WHERE ISBN = @isbn";
                            SqlCommand delBookCmd = new SqlCommand(delBookQuery, con, transaction);
                            delBookCmd.Parameters.AddWithValue("@isbn", isbn);

                            int rows = delBookCmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                transaction.Commit(); // Save changes permanently
                                MessageBox.Show("Book and all available copies deleted successfully.", "Success");
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("ISBN not found.", "Error");
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Something went wrong, undo everything
                            MessageBox.Show("An error occurred during deletion: " + ex.Message);
                        }
                        finally
                        {
                            RefreshGrid();
                        }
                    }
                }
            }
        }
    }
}