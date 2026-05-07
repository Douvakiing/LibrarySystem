using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class AddBookForm : Form
    {
        public AddBookForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text) ||
        string.IsNullOrWhiteSpace(txtTitle.Text) ||
        string.IsNullOrWhiteSpace(txtPages.Text) ||
        string.IsNullOrWhiteSpace(txtCategory.Text) ||
        string.IsNullOrWhiteSpace(txtAuthor.Text) ||
        string.IsNullOrWhiteSpace(txtStaffId.Text) ||
        string.IsNullOrWhiteSpace(txtPublisherId.Text))
            {
                MessageBox.Show("All fields are mandatory. Please fill out the entire form before saving.");
                return; // Stops the code from running the database query
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Query based on your schema
                string query = "INSERT INTO Books (ISBN, Title, NumberOfPages, Category, AuthorName, StaffID, PublisherID) " +
                               "VALUES (@isbn, @title, @pages, @cat, @author, @staff, @pub)";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlCommand addDefaultOneCopy = new SqlCommand("INSERT INTO BookCopy (CopyNumber, ISBN , BookState) VALUES (@copynumber,@isbn,@bookstate)",con);
                cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@pages", int.Parse(txtPages.Text)); // Converts string to int
                cmd.Parameters.AddWithValue("@cat", txtCategory.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@staff", txtStaffId.Text); // Must be a valid StaffID
                cmd.Parameters.AddWithValue("@pub", txtPublisherId.Text);     // Must be a valid PublisherID
                addDefaultOneCopy.Parameters.AddWithValue("@copynumber",1);
                addDefaultOneCopy.Parameters.AddWithValue("@isbn",txtISBN.Text);
                addDefaultOneCopy.Parameters.AddWithValue("@bookstate","Available");

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    addDefaultOneCopy.ExecuteScalar();
                    MessageBox.Show("Book added to library!");
                    this.Close(); // Close the popup after saving
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
