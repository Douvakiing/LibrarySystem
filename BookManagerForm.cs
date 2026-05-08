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
            this.MinimumSize = new System.Drawing.Size(1200, 720);
        }

        private void BookManagerForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                
                // Manually adding columns based on a standard Books table
                dataTable.Columns.Add("ISBN");
                dataTable.Columns.Add("Title");
                dataTable.Columns.Add("NumberOfPages");
                dataTable.Columns.Add("Category");
                dataTable.Columns.Add("AuthorName");
                dataTable.Columns.Add("StaffID");
                dataTable.Columns.Add("PublisherID");

                DataRow row;
                while (reader.Read())
                {
                    row = dataTable.NewRow();
                    row["ISBN"] = reader["ISBN"];
                    row["Title"] = reader["Title"];
                    row["NumberOfPages"] = reader["NumberOfPages"];
                    row["Category"] = reader["Category"];
                    row["AuthorName"] = reader["AuthorName"];
                    row["StaffID"] = reader["StaffID"];
                    row["PublisherID"] = reader["PublisherID"];
                    dataTable.Rows.Add(row);
                }
                reader.Close();
                dgvBooks.DataSource = dataTable;
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
            finally { con.Close(); }
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
        private void btnManageStock_Click(object sender,EventArgs e)
        {
            StockManagerForm popup = new StockManagerForm();
            popup.ShowDialog();
            RefreshGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string isbn = Interaction.InputBox("Enter the ISBN of the book to delete:", "Delete Book", "");

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete book {isbn} and ALL of its copies?", "Confirm Deletion", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(Program.ConnectionString);
                    try
                    {
                        con.Open();

                        // 1. Check if any copies are currently borrowed
                        string checkQuery = "SELECT COUNT(*) FROM BookCopy WHERE ISBN = @isbn AND BookState = 'Borrowed'";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                        
                        SqlParameter pCheckIsbn = new SqlParameter("@isbn", isbn);
                        checkCmd.Parameters.Add(pCheckIsbn);

                        int borrowedCount = (int)checkCmd.ExecuteScalar();

                        if (borrowedCount > 0)
                        {
                            MessageBox.Show($"Action Denied: {borrowedCount} copy/copies of this book are currently borrowed!");
                            return; // Exit the method so it doesn't run the delete commands
                        }

                        // 2. Delete all physical copies of the book FIRST 
                        string delCopiesQuery = "DELETE FROM BookCopy WHERE ISBN = @isbn";
                        SqlCommand delCopiesCmd = new SqlCommand(delCopiesQuery, con);
                        
                        SqlParameter pDelCopyIsbn = new SqlParameter("@isbn", isbn);
                        delCopiesCmd.Parameters.Add(pDelCopyIsbn);
                        
                        delCopiesCmd.ExecuteNonQuery();

                        // 3. Delete the main book record from the Books table
                        string delBookQuery = "DELETE FROM Books WHERE ISBN = @isbn";
                        SqlCommand delBookCmd = new SqlCommand(delBookQuery, con);
                        
                        SqlParameter pDelBookIsbn = new SqlParameter("@isbn", isbn);
                        delBookCmd.Parameters.Add(pDelBookIsbn);

                        int rows = delBookCmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Book and all available copies deleted successfully.", "Success");
                        }
                        else
                        {
                            MessageBox.Show("ISBN not found.", "Error");
                        }
                    }
                    catch (Exception ex) 
                    { 
                        MessageBox.Show("An error occurred during deletion: " + ex.Message); 
                    }
                    finally
                    {
                        // Always refresh the grid and close the connection
                        RefreshGrid();
                        con.Close();
                    }
                }
            }
        }
    }
}