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
    public partial class UpdateBookForm : Form
    {
        public UpdateBookForm()
        {
            InitializeComponent();
        }

        private void chkTitle_CheckedChanged(object sender, EventArgs e)
        {
            txtTitle.Enabled = chkTitle.Checked;
        }

        private void chkCategory_CheckedChanged(object sender, EventArgs e)
        {
            txtCategory.Enabled = chkCategory.Checked;
        }

        private void chkAuthor_CheckedChanged(object sender, EventArgs e)
        {
            txtAuthor.Enabled = chkAuthor.Checked;
        }

        private void chkPages_CheckedChanged(object sender, EventArgs e)
        {
            txtPages.Enabled = chkPages.Checked;
        }

        private void chkStaffId_CheckedChanged(object sender, EventArgs e)
        {
            txtStaffId.Enabled = chkStaffId.Checked;
        }

        private void chkPublishedId_CheckedChanged(object sender, EventArgs e)
        {
            txtPublisherId.Enabled = chkPublishedId.Checked;
        }

        private void chkPubDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpPubDate.Enabled = chkPubDate.Checked;
        }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtISBN.Text))
        {
            MessageBox.Show("Please enter the ISBN of the book you wish to update.", "Missing ISBN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SqlConnection con = new SqlConnection(Program.ConnectionString);
        try
        {
            List<string> columnsToUpdate = new List<string>();
            SqlCommand cmd = new SqlCommand();

            if (chkTitle.Checked)
            {
                columnsToUpdate.Add("Title = @title");
                SqlParameter pTitle = new SqlParameter("@title", txtTitle.Text);
                cmd.Parameters.Add(pTitle);
            }
            if (chkCategory.Checked)
            {
                columnsToUpdate.Add("Category = @cat");
                SqlParameter pCat = new SqlParameter("@cat", txtCategory.Text);
                cmd.Parameters.Add(pCat);
            }
            if (chkAuthor.Checked)
            {
                columnsToUpdate.Add("AuthorName = @author");
                SqlParameter pAuthor = new SqlParameter("@author", txtAuthor.Text);
                cmd.Parameters.Add(pAuthor);
            }
            if (chkPages.Checked)
            {
                if (int.TryParse(txtPages.Text, out int pages) && pages > 0)
                {
                    columnsToUpdate.Add("NumberOfPages = @pages");
                    SqlParameter pPages = new SqlParameter("@pages", pages);
                    cmd.Parameters.Add(pPages);
                }
                else
                {
                    MessageBox.Show("Please enter a valid number greater than 0 for Pages.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
            }
            if (chkStaffId.Checked)
            {
                columnsToUpdate.Add("StaffID = @staff");
                SqlParameter pStaff = new SqlParameter("@staff", txtStaffId.Text);
                cmd.Parameters.Add(pStaff);
            }
            if (chkPublishedId.Checked)
            {
                columnsToUpdate.Add("PublisherID = @pub");
                SqlParameter pPub = new SqlParameter("@pub", txtPublisherId.Text);
                cmd.Parameters.Add(pPub);
            }
            if (chkPubDate.Checked)
            {
                columnsToUpdate.Add("PublicationDate = @date");
                SqlParameter pDate = new SqlParameter("@date", dtpPubDate.Value);
                cmd.Parameters.Add(pDate);
            }

            if (columnsToUpdate.Count == 0)
            {
                MessageBox.Show("No fields selected for update.", "Nothing to do", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "UPDATE Books SET " + string.Join(", ", columnsToUpdate) + " WHERE ISBN = @isbn";
            cmd.CommandText = query;
            cmd.Connection = con;
            
            SqlParameter pIsbn = new SqlParameter("@isbn", txtISBN.Text);
            cmd.Parameters.Add(pIsbn);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Book not found. Check the ISBN.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number == 547)
            {
                if (ex.Message.Contains("PublisherID") || ex.Message.Contains("StaffID"))
                    MessageBox.Show("The Staff ID or Publisher ID you entered does not exist.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Update failed due to database rules (e.g., empty text).", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("SQL Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex) { MessageBox.Show("Application Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        finally { con.Close(); }
    }        
   }
}
