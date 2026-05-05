using System;
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
            // 1. Use the global connection string from Program.cs
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // 2. The SQL Command. Note: I'm omitting PublicationDate for now 
                // since your form doesn't have a date picker yet.
                string query = "INSERT INTO Books (ISBN, Title, NumberOfPages, Category, PublicationDate, AuthorName, StaffID, PublisherID) " +
                               "VALUES (@isbn, @title, @pages, @cat, @pubdate, @author, @staff, @pub)";

                SqlCommand cmd = new SqlCommand(query, con);

                // 3. Bind the values from your UI to the SQL parameters
                cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@cat", txtCategory.Text);
                cmd.Parameters.AddWithValue("@pubdate", dtpPubDate.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@staff", txtStaffId.Text);
                cmd.Parameters.AddWithValue("@pub", txtPublisherId.Text);

                // Convert the "Number of Pages" text to an actual integer for the DB
                int pages = 0;
                int.TryParse(txtPages.Text, out pages);
                cmd.Parameters.AddWithValue("@pages", pages);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery(); // Execute the insert
                    MessageBox.Show("Book added successfully!");

                    // 4. Refresh the grid so you see the new book immediately!
                    RefreshGrid();

                    // Clear the boxes for the next entry
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    // This will catch things like duplicate ISBNs or missing Foreign Keys
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }

        // Helper method to clear the text boxes after a successful add
        private void ClearInputs()
        {
            txtISBN.Clear();
            txtTitle.Clear();
            txtCategory.Clear();
            txtAuthor.Clear();
            txtPages.Clear();
            txtStaffId.Clear();
            txtPublisherId.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}