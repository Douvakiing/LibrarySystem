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
            // Remove the 'using' block and instantiate normally
            SqlConnection con = new SqlConnection(Program.ConnectionString);

            try
            {
                con.Open(); 

                // --- PRE-CHECK 1: Does the Staff exist? ---
                SqlCommand checkStaff = new SqlCommand("SELECT COUNT(*) FROM Staff WHERE StaffID = @staffId", con);
                SqlParameter paramStaffIdCheck = new SqlParameter("@staffId", staffId);
                checkStaff.Parameters.Add(paramStaffIdCheck);

                if ((int)checkStaff.ExecuteScalar() == 0)
                {
                    MessageBox.Show($"Staff ID {staffId} does not exist.", "Invalid Staff ID");
                    con.Close(); // Remember to manually close before returning!
                    return; 
                }

                // --- PRE-CHECK 2: Does the Publisher exist? ---
                SqlCommand checkPub = new SqlCommand("SELECT COUNT(*) FROM Publisher WHERE PublisherID = @pubId", con);
                SqlParameter paramPubIdCheck = new SqlParameter("@pubId", pubId);
                checkPub.Parameters.Add(paramPubIdCheck);

                if ((int)checkPub.ExecuteScalar() == 0)
                {
                    MessageBox.Show($"Publisher ID {pubId} does not exist.", "Invalid Publisher ID");
                    con.Close(); // Remember to manually close before returning!
                    return; 
                }

                // --- Actual Insert ---
                string query = "INSERT INTO Books (ISBN, Title, NumberOfPages, Category, AuthorName, StaffID, PublisherID) " +
                            "VALUES (@isbn, @title, @pages, @cat, @author, @staff, @pub)";

                SqlCommand cmd = new SqlCommand(query, con);
                
                // Professor's strict parameter style
                SqlParameter paramIsbn = new SqlParameter("@isbn", txtISBN.Text);
                cmd.Parameters.Add(paramIsbn);
                
                SqlParameter paramTitle = new SqlParameter("@title", txtTitle.Text);
                cmd.Parameters.Add(paramTitle);
                
                SqlParameter paramPages = new SqlParameter("@pages", pages);
                cmd.Parameters.Add(paramPages);
                
                SqlParameter paramCat = new SqlParameter("@cat", txtCategory.Text);
                cmd.Parameters.Add(paramCat);
                
                SqlParameter paramAuthor = new SqlParameter("@author", txtAuthor.Text);
                cmd.Parameters.Add(paramAuthor);
                
                SqlParameter paramStaff = new SqlParameter("@staff", staffId);
                cmd.Parameters.Add(paramStaff);
                
                SqlParameter paramPub = new SqlParameter("@pub", pubId);
                cmd.Parameters.Add(paramPub);

                SqlCommand addDefaultOneCopy = new SqlCommand("INSERT INTO BookCopy (CopyNumber, ISBN, BookState) VALUES (@copynumber, @isbnCopy, @bookstate)", con);
                
                SqlParameter paramCopyNum = new SqlParameter("@copynumber", 1);
                addDefaultOneCopy.Parameters.Add(paramCopyNum);
                
                SqlParameter paramIsbnCopy = new SqlParameter("@isbnCopy", txtISBN.Text);
                addDefaultOneCopy.Parameters.Add(paramIsbnCopy);
                
                SqlParameter paramState = new SqlParameter("@bookstate", "Available");
                addDefaultOneCopy.Parameters.Add(paramState);

                cmd.ExecuteNonQuery();
                addDefaultOneCopy.ExecuteNonQuery();

                MessageBox.Show("Book added to library successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error");
            }
            finally
            {
                // The professor's style requires explicit closing in finally blocks
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            }
        }
    }