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
            // 1. Validation (Keep this, it's good!)
            // Note: I removed txtMemberID from validation because SQL handles it automatically.
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("All fields are mandatory. Please fill out the entire form before saving.");
                return;
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // 2. The SQL Query
                string query = "INSERT INTO Member (MemberID, FirstName, LastName, Email, Phone, Address, MembershipDate) " +
                               "VALUES (@memberId, @firstName, @lastName, @email, @phone, @address, @date)";

                SqlCommand cmd = new SqlCommand(query, con);

                // 3. Mapping Parameters
                cmd.Parameters.AddWithValue("@memberId", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);

                // FIXED: Removed the int.Parse here. Last names are strings!
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);

                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                cmd.Parameters.AddWithValue("@date", dtpMemberDate.Value);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // FIXED: Changed "Book" to "Member"
                    MessageBox.Show("New member successfully registered!");
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
    

