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
                con.Open();

                // --- UNIFIED PRE-CHECK: STAFF (Only if checked) ---
                if (chkStaffId.Checked)
                {
                    if (!int.TryParse(txtStaffId.Text, out int sId))
                    {
                        MessageBox.Show("Staff ID must be a number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    SqlCommand checkStaff = new SqlCommand("SELECT COUNT(*) FROM Staff WHERE StaffID = @id", con);
                    SqlParameter pCheckStaff = new SqlParameter("@id", sId);
                    checkStaff.Parameters.Add(pCheckStaff);
                    
                    if ((int)checkStaff.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"Staff ID '{sId}' does not exist in the system. Please check your spelling.", "Invalid Staff ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // --- UNIFIED PRE-CHECK: PUBLISHER (Only if checked) ---
                if (chkPublishedId.Checked)
                {
                    if (!int.TryParse(txtPublisherId.Text, out int pId))
                    {
                        MessageBox.Show("Publisher ID must be a number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    SqlCommand checkPub = new SqlCommand("SELECT COUNT(*) FROM Publisher WHERE PublisherID = @id", con);
                    SqlParameter pCheckPub = new SqlParameter("@id", pId);
                    checkPub.Parameters.Add(pCheckPub);
                    
                    if ((int)checkPub.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"Publisher ID '{pId}' does not exist in the system. Please verify the ID.", "Invalid Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // --- BUILD UPDATE ---
                List<string> updates = new List<string>();
                SqlCommand cmd = new SqlCommand();

                if (chkTitle.Checked) 
                { 
                    updates.Add("Title = @title"); 
                    SqlParameter pTitle = new SqlParameter("@title", txtTitle.Text);
                    cmd.Parameters.Add(pTitle); 
                }
                if (chkCategory.Checked) 
                { 
                    updates.Add("Category = @cat"); 
                    SqlParameter pCat = new SqlParameter("@cat", txtCategory.Text);
                    cmd.Parameters.Add(pCat); 
                }
                if (chkAuthor.Checked) 
                { 
                    updates.Add("AuthorName = @author"); 
                    SqlParameter pAuthor = new SqlParameter("@author", txtAuthor.Text);
                    cmd.Parameters.Add(pAuthor); 
                }
                if (chkPages.Checked)
                {
                    if (int.TryParse(txtPages.Text, out int pg) && pg > 0) 
                    { 
                        updates.Add("NumberOfPages = @pg"); 
                        SqlParameter pPg = new SqlParameter("@pg", pg);
                        cmd.Parameters.Add(pPg); 
                    }
                    else 
                    { 
                        MessageBox.Show("Please enter a valid number greater than 0 for Pages.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                        return; 
                    }
                }
                if (chkStaffId.Checked) 
                { 
                    updates.Add("StaffID = @sid"); 
                    SqlParameter pSid = new SqlParameter("@sid", txtStaffId.Text);
                    cmd.Parameters.Add(pSid); 
                }
                if (chkPublishedId.Checked) 
                { 
                    updates.Add("PublisherID = @pid"); 
                    SqlParameter pPid = new SqlParameter("@pid", txtPublisherId.Text);
                    cmd.Parameters.Add(pPid); 
                }
                if (chkPubDate.Checked) 
                { 
                    updates.Add("PublicationDate = @dt"); 
                    SqlParameter pDt = new SqlParameter("@dt", dtpPubDate.Value);
                    cmd.Parameters.Add(pDt); 
                }

                if (updates.Count == 0)
                {
                    MessageBox.Show("No fields selected for update.", "Nothing to do", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cmd.CommandText = $"UPDATE Books SET {string.Join(", ", updates)} WHERE ISBN = @isbn";
                cmd.Connection = con;
                
                SqlParameter pTargetIsbn = new SqlParameter("@isbn", txtISBN.Text);
                cmd.Parameters.Add(pTargetIsbn);

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
                    MessageBox.Show("Database rule violated! Please ensure all text fields have actual words and not just spaces.", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("SQL Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            { 
                con.Close(); 
            }
        }
     }
}
