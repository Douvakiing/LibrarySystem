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

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
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
                        checkStaff.Parameters.AddWithValue("@id", sId);
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
                        checkPub.Parameters.AddWithValue("@id", pId);
                        if ((int)checkPub.ExecuteScalar() == 0)
                        {
                            MessageBox.Show($"Publisher ID '{pId}' does not exist in the system. Please verify the ID.", "Invalid Publisher ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // --- BUILD UPDATE ---
                    List<string> updates = new List<string>();
                    SqlCommand cmd = new SqlCommand();

                    if (chkTitle.Checked) { updates.Add("Title = @title"); cmd.Parameters.AddWithValue("@title", txtTitle.Text); }
                    if (chkCategory.Checked) { updates.Add("Category = @cat"); cmd.Parameters.AddWithValue("@cat", txtCategory.Text); }
                    if (chkAuthor.Checked) { updates.Add("AuthorName = @author"); cmd.Parameters.AddWithValue("@author", txtAuthor.Text); }
                    if (chkPages.Checked)
                    {
                        if (int.TryParse(txtPages.Text, out int pg) && pg > 0) { updates.Add("NumberOfPages = @pg"); cmd.Parameters.AddWithValue("@pg", pg); }
                        else { MessageBox.Show("Please enter a valid number greater than 0 for Pages.", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    }
                    if (chkStaffId.Checked) { updates.Add("StaffID = @sid"); cmd.Parameters.AddWithValue("@sid", txtStaffId.Text); }
                    if (chkPublishedId.Checked) { updates.Add("PublisherID = @pid"); cmd.Parameters.AddWithValue("@pid", txtPublisherId.Text); }
                    if (chkPubDate.Checked) { updates.Add("PublicationDate = @dt"); cmd.Parameters.AddWithValue("@dt", dtpPubDate.Value); }

                    if (updates.Count == 0)
                    {
                        MessageBox.Show("No fields selected for update.", "Nothing to do", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    cmd.CommandText = $"UPDATE Books SET {string.Join(", ", updates)} WHERE ISBN = @isbn";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else MessageBox.Show("Book not found. Check the ISBN.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            }
        }
    }
}
