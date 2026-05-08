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
            // 1. Basic empty text validation
            if (string.IsNullOrWhiteSpace(txtISBN.Text) ||
                string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtPages.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtStaffId.Text) ||
                string.IsNullOrWhiteSpace(txtPublisherId.Text))
            {
                MessageBox.Show("All fields are mandatory. Please fill out the entire form before saving.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Pre-SQL Validation: Safely check numeric types
            if (!int.TryParse(txtPages.Text, out int pages) || pages <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for 'Number of Pages'.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtStaffId.Text, out int sId))
            {
                MessageBox.Show("Staff ID must be a number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtPublisherId.Text, out int pId))
            {
                MessageBox.Show("Publisher ID must be a number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                try
                {
                    con.Open();

                    // --- UNIFIED PRE-CHECK: STAFF ---
                    SqlCommand checkStaff = new SqlCommand("SELECT COUNT(*) FROM Staff WHERE StaffID = @id", con);
                    checkStaff.Parameters.AddWithValue("@id", sId);
                    if ((int)checkStaff.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"Staff ID '{sId}' does not exist in the system. Please check your spelling.", "Invalid Staff ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // --- UNIFIED PRE-CHECK: PUBLISHER ---
                    SqlCommand checkPub = new SqlCommand("SELECT COUNT(*) FROM Publisher WHERE PublisherID = @id", con);
                    checkPub.Parameters.AddWithValue("@id", pId);
                    if ((int)checkPub.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"Publisher ID '{pId}' does not exist in the system. Please verify the ID.", "Invalid Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // --- EXECUTE INSERT ---
                    string query = "INSERT INTO Books (ISBN, Title, NumberOfPages, Category, AuthorName, StaffID, PublisherID) " +
                                   "VALUES (@isbn, @title, @pages, @cat, @author, @staff, @pub)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@pages", pages);
                    cmd.Parameters.AddWithValue("@cat", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                    cmd.Parameters.AddWithValue("@staff", sId);
                    cmd.Parameters.AddWithValue("@pub", pId);

                    SqlCommand addCopy = new SqlCommand("INSERT INTO BookCopy (CopyNumber, ISBN, BookState) VALUES (1, @isbn, 'Available')", con);
                    addCopy.Parameters.AddWithValue("@isbn", txtISBN.Text);

                    cmd.ExecuteNonQuery();
                    addCopy.ExecuteNonQuery();

                    MessageBox.Show("Book added to library successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                        MessageBox.Show("A book with this ISBN already exists in the system!", "Duplicate ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (ex.Number == 547)
                        MessageBox.Show("Database rule violated! Please ensure all text fields have actual words and not just spaces.", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("SQL Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}