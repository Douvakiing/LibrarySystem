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
            this.Close();
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
            // 1. Prompt for ISBN
            string isbn = Interaction.InputBox("Enter the ISBN of the book to delete:", "Delete Book", "");

            if (!string.IsNullOrWhiteSpace(isbn))
            {
                // 2. Confirm
                var confirm = MessageBox.Show($"Delete book {isbn}?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Program.ConnectionString))
                    {
                        string query = "DELETE FROM Books WHERE ISBN = @isbn";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@isbn", isbn);

                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0) MessageBox.Show("Deleted.");
                        else MessageBox.Show("ISBN not found.");
                        RefreshGrid();
                    }
                }
            }
        }
    }
}