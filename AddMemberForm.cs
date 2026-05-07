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
    public partial class AddMemberForm : Form
    {
        public AddMemberForm()
        {
            InitializeComponent();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            // 1. Basic empty text validation
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("All fields are mandatory. Please fill out the entire form before saving.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Pre-SQL Validation: Safely check MemberID so C# doesn't crash, and ensure it's > 0
            if (!int.TryParse(txtMemberID.Text, out int memberId) || memberId <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for Member ID.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // 3. The SQL Query
                string query = "INSERT INTO Member (MemberID, FirstName, LastName, Email, Phone, Address, MembershipDate) " +
                               "VALUES (@memberId, @firstName, @lastName, @email, @phone, @address, @date)";

                SqlCommand cmd = new SqlCommand(query, con);

                // 4. Mapping Parameters
                cmd.Parameters.AddWithValue("@memberId", memberId); // Using the safely parsed variable
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@date", dtpMemberDate.Value);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("New member successfully registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    // THE DIPLOMAT: Translating SQL Errors to English
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("A member with this ID already exists in the system!", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (ex.Number == 547) // Constraint Violation
                    {
                        MessageBox.Show("Database rule violated! Please ensure names aren't just spaces and the ID is valid.", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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