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
            txtPublisherId.Enabled = chkPages.Checked;
        }

        private void chkPubDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpPubDate.Enabled = chkPubDate.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("Please enter the ISBN of the book you wish to update.");
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // 1. Build the dynamic SET clause
                List<string> columnsToUpdate = new List<string>();
                SqlCommand cmd = new SqlCommand();

                if (chkTitle.Checked)
                {
                    columnsToUpdate.Add("Title = @title");
                    cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                }
                if (chkCategory.Checked)
                {
                    columnsToUpdate.Add("Category = @cat");
                    cmd.Parameters.AddWithValue("@cat", txtCategory.Text);
                }
                if (chkAuthor.Checked)
                {
                    columnsToUpdate.Add("AuthorName = @author"); // Matches schema
                    cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                }
                if (chkPages.Checked)
                {
                    columnsToUpdate.Add("NumberOfPages = @pages"); // Matches schema
                    cmd.Parameters.AddWithValue("@pages", int.Parse(txtPages.Text));
                }
                if (chkStaffId.Checked)
                {
                    columnsToUpdate.Add("StaffID = @staff"); // Foreign Key
                    cmd.Parameters.AddWithValue("@staff", txtStaffId.Text);
                }
                if (chkPublishedId.Checked)
                {
                    columnsToUpdate.Add("PublisherID = @pub"); // Foreign Key
                    cmd.Parameters.AddWithValue("@pub", txtPublisherId.Text);
                }
                if (dtpPubDate.Checked)
                {
                    columnsToUpdate.Add("PublicationDate = @date");
                    cmd.Parameters.AddWithValue("@date", dtpPubDate.Value);
                }

                if (columnsToUpdate.Count == 0)
                {
                    MessageBox.Show("No fields selected for update.");
                    return;
                }

                // 2. Finalize the Query
                string query = "UPDATE Books SET " + string.Join(", ", columnsToUpdate) + " WHERE ISBN = @isbn";
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);

                try
                {
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0) MessageBox.Show("Book updated successfully!");
                    else MessageBox.Show("Book not found.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
