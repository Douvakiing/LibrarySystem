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
            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Query based on your schema
                string query = "INSERT INTO Books (ISBN, Title, Language, NumberOfPages, Category, AuthorName, StaffID, PublisherID) " +
                               "VALUES (@isbn, @title, @lang, @pages, @cat, @author, @staff, @pub)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@pages", int.Parse(txtPages.Text)); // Converts string to int
                cmd.Parameters.AddWithValue("@cat", txtCategory.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@staff", txtStaffId.Text); // Must be a valid StaffID
                cmd.Parameters.AddWithValue("@pub", txtPublisherId.Text);     // Must be a valid PublisherID

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book added to library!");
                    this.Close(); // Close the popup after saving
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
