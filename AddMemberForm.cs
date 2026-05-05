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
            if (string.IsNullOrWhiteSpace(txtMemberID.Text) ||
            string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtPhone.Text) ||
            string.IsNullOrWhiteSpace(txtAddress.Text) ||
            string.IsNullOrWhiteSpace(dtpMemberDate.Text))
            {
                MessageBox.Show("All fields are mandatory. Please fill out the entire form before saving.");
                return; // Stops the code from running the database query
            }

            using (SqlConnection con = new SqlConnection(Program.ConnectionString))
            {
                // Query based on your schema
                string query = "INSERT INTO Member (MemberID, FirstName, LastName, Email, Phone, Address, MembershipDate) " +
                               "VALUES (@memberId, @firstName, @lastName, @email, @phone, @address, @date)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@memberId", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lastName", int.Parse(txtLastName.Text)); // Converts string to int
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text); // Must be a valid StaffID
                cmd.Parameters.AddWithValue("@date", dtpMemberDate.Text);     // Must be a valid PublisherID

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book added to library!");
                    this.Close(); // Close the popup after saving
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
    

