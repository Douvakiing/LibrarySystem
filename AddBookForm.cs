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

            // 2. Pre-SQL Validation: Safely check the pages number so C# doesn't crash
            if (!int.TryParse(txtPages.Text, out int pages) || pages <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for 'Number of Pages'.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Pre-SQL Validation: Safely check IDs so C# doesn't crash if letters are typed
            if (!int.TryParse(txtStaffId.Text, out int staffId))
            {
                MessageBox.Show("Please enter a valid numeric Staff ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtPublisherId.Text, out int pubId))
            {
                MessageBox.Show("Please enter a valid numeric Publisher ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                try
                {
                    con.Open(); // Open the connection early so we can run our pre-checks

                    // --- PRE-CHECK 1: Does the Staff exist? ---
                    SqlCommand checkStaff = new SqlCommand("SELECT COUNT(*) FROM Staff WHERE StaffID = @staffId", con);
                    checkStaff.Parameters.AddWithValue("@staffId", staffId);

                    if ((int)checkStaff.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"Staff ID {staffId} does not exist in the system. Please check your spelling.", "Invalid Staff ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Stop the save process entirely
                    }

                    // --- PRE-CHECK 2: Does the Publisher exist? ---
                    SqlCommand checkPub = new SqlCommand("SELECT COUNT(*) FROM Publisher WHERE PublisherID = @pubId", con);
                    checkPub.Parameters.AddWithValue("@pubId", pubId);

                    if ((int)checkPub.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"Publisher ID {pubId} does not exist in the system. Please verify the ID.", "Invalid Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Stop the save process entirely
                    }

                    // --- ALL CLEAR: Proceed with the actual Insert ---
                    string query = "INSERT INTO Books (ISBN, Title, NumberOfPages, Category, AuthorName, StaffID, PublisherID) " +
                                   "VALUES (@isbn, @title, @pages, @cat, @author, @staff, @pub)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlCommand addDefaultOneCopy = new SqlCommand("INSERT INTO BookCopy (CopyNumber, ISBN, BookState) VALUES (@copynumber, @isbn, @bookstate)", con);

                    cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@pages", pages); // Using the safely parsed variable
                    cmd.Parameters.AddWithValue("@cat", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                    cmd.Parameters.AddWithValue("@staff", staffId); // Using the safely parsed variable
                    cmd.Parameters.AddWithValue("@pub", pubId);     // Using the safely parsed variable

                    addDefaultOneCopy.Parameters.AddWithValue("@copynumber", 1);
                    addDefaultOneCopy.Parameters.AddWithValue("@isbn", txtISBN.Text);
                    addDefaultOneCopy.Parameters.AddWithValue("@bookstate", "Available");

                    cmd.ExecuteNonQuery();
                    addDefaultOneCopy.ExecuteNonQuery();

                    MessageBox.Show("Book added to library successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    // THE DIPLOMAT: Translating SQL Errors to English
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("A book with this ISBN already exists in the system!", "Duplicate ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (ex.Number == 547)
                    {
                        // We already checked the Foreign Keys, so if we hit a 547 here, it's likely a CHECK constraint (e.g., negative numbers, empty strings that bypassed C#)
                        MessageBox.Show("Database rule violated! Please ensure all text fields have actual words and not just spaces.", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("SQL Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Application Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}