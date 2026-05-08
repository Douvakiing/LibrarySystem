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
    public partial class UpdateMemberForm : Form
    {
        public UpdateMemberForm()
        {
            InitializeComponent();
        }

        private void chkFirstName_CheckedChanged(object sender, EventArgs e)
        {
            txtFirstName.Enabled = chkFirstName.Checked;
        }

        private void chkLastName_CheckedChanged(object sender, EventArgs e)
        {
            txtLastName.Enabled = chkLastName.Checked;
        }

        private void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEmail.Enabled = chkEmail.Checked;
        }

        private void chkPhone_CheckedChanged(object sender, EventArgs e)
        {
            txtPhone.Enabled = chkPhone.Checked;
        }

        private void chkAddress_CheckedChanged(object sender, EventArgs e)
        {
            txtAddress.Enabled = chkAddress.Checked;
        }

        private void chkMembershipDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpMemberDate.Enabled = chkMembershipDate.Checked;
        }

        private void btnUpdateMember_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMemberID.Text, out int memberId) || memberId <= 0)
            {
                MessageBox.Show("Please enter a valid positive Member ID to update.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection(Program.ConnectionString);
            try
            {
                List<string> columnsToUpdate = new List<string>();
                SqlCommand cmd = new SqlCommand();

                if (chkFirstName.Checked)
                {
                    columnsToUpdate.Add("FirstName = @fn");
                    SqlParameter pFn = new SqlParameter("@fn", txtFirstName.Text);
                    cmd.Parameters.Add(pFn);
                }
                if (chkLastName.Checked)
                {
                    columnsToUpdate.Add("LastName = @ln");
                    SqlParameter pLn = new SqlParameter("@ln", txtLastName.Text);
                    cmd.Parameters.Add(pLn);
                }
                if (chkEmail.Checked)
                {
                    columnsToUpdate.Add("Email = @email");
                    SqlParameter pEmail = new SqlParameter("@email", txtEmail.Text);
                    cmd.Parameters.Add(pEmail);
                }
                if (chkPhone.Checked)
                {
                    columnsToUpdate.Add("Phone = @phone");
                    SqlParameter pPhone = new SqlParameter("@phone", txtPhone.Text);
                    cmd.Parameters.Add(pPhone);
                }
                if (chkAddress.Checked)
                {
                    columnsToUpdate.Add("Address = @addr");
                    SqlParameter pAddr = new SqlParameter("@addr", txtAddress.Text);
                    cmd.Parameters.Add(pAddr);
                }
                if (chkMembershipDate.Checked)
                {
                    columnsToUpdate.Add("MembershipDate = @date");
                    SqlParameter pDate = new SqlParameter("@date", dtpMemberDate.Value);
                    cmd.Parameters.Add(pDate);
                }

                if (columnsToUpdate.Count == 0)
                {
                    MessageBox.Show("No fields selected for update.", "Nothing to do", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string query = "UPDATE Member SET " + string.Join(", ", columnsToUpdate) + " WHERE MemberID = @id";
                cmd.CommandText = query;
                cmd.Connection = con;
                
                SqlParameter pId = new SqlParameter("@id", memberId);
                cmd.Parameters.Add(pId);

                con.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Member record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Member ID not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) MessageBox.Show("Update failed due to database rules. Ensure names aren't empty.", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("SQL Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show("Application Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { con.Close(); }
}    
     }
}